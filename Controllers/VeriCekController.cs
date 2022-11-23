using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProjesiErp.Data;
using BitirmeProjesiErp.Models;
using System.Dynamic;

namespace BitirmeProjesiErp.Controllers
{
    public class VeriCekController : Controller
    {
        private readonly scfContext _scfcontext;
        private readonly WSContext _context;
        public VeriCekController(scfContext scfcontext,WSContext context)
        {
            _context = context;
            _scfcontext = scfcontext;
        }

        public async Task<IActionResult> VeriEsle(
            WSModel webServisBilgi,
            CariKart cariKart)
        {
            
            webServisBilgi = await _context.WebServisBilgisi
                .FirstOrDefaultAsync(m => m.WSID == 1);
            Sabitler.DiaLogin(webServisBilgi);
            Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
            Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
            #region CarileriVeriTabanınaYaz

            dynamic response = Sabitler.sendMessageToServer(scf_listele(webServisBilgi, "scf_carikart_listele"), Sabitler.scfEk);
            

            if (response == null)
            {
                Console.WriteLine("Cariler getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (response.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.carikartlistesi = response.result;
                Console.WriteLine(response.result + response.msg);
                dynamic carilerObj = response.result;
                int temp = carilerObj.Count;

                int i = 0;
                for (; i < temp; i++)
                {
                    string temp2 = carilerObj[i]._key;
                    bool flag = await _scfcontext.CariKarts.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    cariKart = await _scfcontext.CariKarts.FirstOrDefaultAsync(u => u._key == temp2);
                    CariKart Cari = new CariKart();
                    //Cari._key = temp2;
                    //Cari.unvan = carilerObj[i].unvan;
                    //UpdateCari(Cari, temp2, flag);
                    if (flag)
                    {
                        CariKart Cariupdate = await _scfcontext.CariKarts.FirstOrDefaultAsync(u => u._key == temp2);
                        Cariupdate.unvan = carilerObj[i].unvan;
                        //UpdateCari(Cari, temp2, flag);
                        _scfcontext.CariKarts.Update(Cariupdate);
                        await _scfcontext.SaveChangesAsync();
                        try
                        {
                            await _scfcontext.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            ex.Entries.Single().Reload();
                            await _scfcontext.SaveChangesAsync();

                        }
                    }

                    else
                    {
                        //CariKart Cari = new CariKart();
                        Cari._key = carilerObj[i]._key;
                        Cari.unvan = carilerObj[i].unvan;
                        _scfcontext.CariKarts.Add(Cari);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (response.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + response.code + " Message: " + response.msg);

            }
            else
            {
                Console.WriteLine("Cariler getirilirken bir hata oluştu. Code: " + response.code + " Message: " + response.msg);

                return Ok();
            }
            #endregion
            return Ok();
        }
        public ExpandoObject scf_listele(WSModel webServisBilgi,string servis)
        {
            dynamic request = new ExpandoObject();
            ((IDictionary<string, object>)request)[servis] = Sabitler.session_id;
            request.scf_carikart_listele = new ExpandoObject();
            request.scf_carikart_listele.session_id = Sabitler.session_id;
            request.scf_carikart_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_carikart_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        //public bool UpdateCari(CariKart Cari, string id,bool flag)// eğer var ise cariyi günceller.
        //{

        //    CariKart cariKart = _scfcontext.CariKarts.FirstOrDefault(u => u._key == id);
        //    var alper = 123;
        //    if(cariKart == null)
        //    {
        //        cariKart.unvan = Cari.unvan;
        //        cariKart._key = Cari._key;
        //        _scfcontext.CariKarts.Add(cariKart);
        //        _scfcontext.SaveChanges();
        //    }
            

        //    if (flag)
        //    {
        //        _scfcontext.CariKarts.Update(cariKart);
        //        _scfcontext.SaveChanges();
        //    }
        //    else
        //    {
                
        //    }
        //    return true;
        //}
    }
}
