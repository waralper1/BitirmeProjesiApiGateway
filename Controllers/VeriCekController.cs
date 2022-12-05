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
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Globalization;

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
            CariKart cariKart,
            StokKart stokKart,
            SatisElemanlari satiselemanlariKart,
            TeklifKalemi teklifKalemiKart,
            OdemePlani odemePlani,
            CariKartAdresler cariKartAdresler,
            CariKartYetkili cariKartYetkili,
            Rpr_dinamik_raporparametreleri_getir tasarimlarKart,//tam burada seda aradı denemelik model oluşturdum test edip geri donden veriye gore modeli tekrar yapcagım
            Doviz doviz
            )
        {
            
            webServisBilgi = await _context.WebServisBilgisi
                .FirstOrDefaultAsync(m => m.WSID == 1);
            Sabitler.DiaLogin(webServisBilgi);
            Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
            Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
            #region CarileriVeriTabanınaYaz

            dynamic response = Sabitler.sendMessageToServer(scf_carikart_listele(webServisBilgi), Sabitler.scfEk);
            

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
                    if (flag)
                    {
                        CariKart Cariupdate = await _scfcontext.CariKarts.FirstOrDefaultAsync(u => u._key == temp2);
                        Cariupdate.unvan = carilerObj[i].unvan;
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
            #region StoklarıveriTabanınaYaz
            dynamic stkresponse = Sabitler.sendMessageToServer(scf_stokkart_listele(webServisBilgi), Sabitler.scfEk);


            if (stkresponse == null)
            {
                Console.WriteLine("Stoklar getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (stkresponse.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
                Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
                Sabitler.stokkartlistesi = stkresponse.result;
                Console.WriteLine(stkresponse.result + stkresponse.msg);
                dynamic stoklarObj = stkresponse.result;
                int temp = stoklarObj.Count;
                int i = 0;
                for (; i < temp; i++)
                {
                    string temp2 = stoklarObj[i]._key;
                    bool flag = await _scfcontext.StokKarts.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    stokKart = await _scfcontext.StokKarts.FirstOrDefaultAsync(u => u._key == temp2);
                    if (flag)//update
                    {
                        StokKart Stokupdate =  await _scfcontext.StokKarts.FirstOrDefaultAsync(u => u._key == temp2);
                        Stokupdate.stokkartkodu = stoklarObj[i].stokkartkodu;
                        Stokupdate.aciklama = stoklarObj[i].aciklama;
                        Stokupdate.birimisimleri = stoklarObj[i].birimisimleri;
                        Stokupdate.birimkeyleri = stoklarObj[i].birimkeyleri;
                        _scfcontext.StokKarts.Update(Stokupdate);
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
                        StokKart Stok = new StokKart();
                        Stok._key = stoklarObj[i]._key;
                        Stok.stokkartkodu = stoklarObj[i].stokkartkodu;
                        Stok.aciklama = stoklarObj[i].aciklama;
                        Stok.birimisimleri = stoklarObj[i].birimisimleri;
                        Stok.birimkeyleri = stoklarObj[i].birimkeyleri;
                        _scfcontext.StokKarts.Add(Stok);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (stkresponse.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + stkresponse.code + " Message: " + stkresponse.msg);

            }
            else
            {
                Console.WriteLine("Stoklar getirilirken bir hata oluştu. Code: " + stkresponse.code + " Message: " + stkresponse.msg);

                return Ok();
            }
            #endregion
            #region scf_satiselemani_listele
            dynamic stsresponse = Sabitler.sendMessageToServer(scf_satiselemani_listele(webServisBilgi), Sabitler.scfEk);


            if (stsresponse == null)
            {
                Console.WriteLine("Stoklar getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (stsresponse.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
                Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
                Sabitler.tekliflistesi = stsresponse.result;
                Console.WriteLine(stsresponse.result + stsresponse.msg);
                dynamic SatisElemanObj = stsresponse.result;
                int temp = SatisElemanObj.Count;
                int i = 0;
                for (; i < temp; i++)
                {
                    string temp2 = SatisElemanObj[i]._key;
                    bool flag = await _scfcontext.SatisElemanlaris.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    satiselemanlariKart = await _scfcontext.SatisElemanlaris.FirstOrDefaultAsync(u => u._key == temp2);
                    if (flag)
                    {
                        SatisElemanlari satiselemanlari = await _scfcontext.SatisElemanlaris.FirstOrDefaultAsync(u => u._key == temp2);

                        satiselemanlari.aciklama = SatisElemanObj[i].aciklama;
                        satiselemanlari.ceptel = SatisElemanObj[i].ceptel;
                        satiselemanlari._key_scf_carikart = SatisElemanObj[i]._key_scf_carikart;
                        satiselemanlari.durum = SatisElemanObj[i].durum;
                        satiselemanlari.kodu = SatisElemanObj[i].kodu;

                        _scfcontext.SatisElemanlaris.Update(satiselemanlari);
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
                        SatisElemanlari satiselemanlari = new SatisElemanlari();
                        satiselemanlari._key = SatisElemanObj[i]._key;
                        satiselemanlari.aciklama = SatisElemanObj[i].aciklama;
                        satiselemanlari.ceptel = SatisElemanObj[i].ceptel;
                        satiselemanlari._key_scf_carikart = SatisElemanObj[i]._key_scf_carikart;
                        satiselemanlari.durum = SatisElemanObj[i].durum;
                        satiselemanlari.kodu = SatisElemanObj[i].kodu;



                        _scfcontext.SatisElemanlaris.Add(satiselemanlari);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (stsresponse.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + stsresponse.code + " Message: " + stsresponse.msg);

            }
            else
            {
                Console.WriteLine("Stoklar getirilirken bir hata oluştu. Code: " + stsresponse.code + " Message: " + stsresponse.msg);

                return Ok();
            }
            #endregion
            #region scf_odeme_plani_listele
            dynamic oplnresponse = Sabitler.sendMessageToServer(scf_odeme_plani_listele(webServisBilgi), Sabitler.scfEk);


            if (oplnresponse == null)
            {
                Console.WriteLine("Stoklar getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (oplnresponse.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
                Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
                Console.WriteLine(oplnresponse.result + oplnresponse.msg);
                dynamic OplnElemanObj = oplnresponse.result;
                int temp = OplnElemanObj.Count;
                int i = 0;
                for (; i < temp; i++)
                {
                    string temp2 = OplnElemanObj[i]._key;
                    bool flag = await _scfcontext.OdemePlanis.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    odemePlani = await _scfcontext.OdemePlanis.FirstOrDefaultAsync(u => u._key == temp2);
                    if (flag)
                    {
                        OdemePlani odemePlani1 = await _scfcontext.OdemePlanis.FirstOrDefaultAsync(u => u._key == temp2);
                        odemePlani1.Aciklama = OplnElemanObj[i].aciklama;
                        odemePlani1.Kodu = OplnElemanObj[i].kodu;


                        _scfcontext.OdemePlanis.Update(odemePlani1);
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
                        OdemePlani odemePlani1 = new OdemePlani();
                        odemePlani1._key = OplnElemanObj[i]._key;
                        odemePlani1.Aciklama = OplnElemanObj[i].aciklama;
                        odemePlani1.Kodu = OplnElemanObj[i].kodu;



                        _scfcontext.OdemePlanis.Add(odemePlani1);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (oplnresponse.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + oplnresponse.code + " Message: " + oplnresponse.msg);

            }
            else
            {
                Console.WriteLine("Stoklar getirilirken bir hata oluştu. Code: " + oplnresponse.code + " Message: " + oplnresponse.msg);

                return Ok();
            }
            #endregion
            #region scf_carikart_adresleri_listele
            dynamic calresponse = Sabitler.sendMessageToServer(scf_carikart_adresleri_listele(webServisBilgi), Sabitler.scfEk);


            if (calresponse == null)
            {
                Console.WriteLine("Stoklar getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (calresponse.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
                Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
                Console.WriteLine(calresponse.result + calresponse.msg);
                dynamic calElemanObj = calresponse.result;
                int temp = calElemanObj.Count;
                int i = 0;
                for (; i < temp; i++)
                {
                    string temp2 = calElemanObj[i]._key;
                    bool flag = await _scfcontext.CariKartAdreslers.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    cariKartAdresler = await _scfcontext.CariKartAdreslers.FirstOrDefaultAsync(u => u._key == temp2);
                    if (flag)
                    {
                        CariKartAdresler cariKartAdresler1 = await _scfcontext.CariKartAdreslers.FirstOrDefaultAsync(u => u._key == temp2);
                        cariKartAdresler1.adresadi = calElemanObj[i].adresadi;
                        cariKartAdresler1.carikartunvani = calElemanObj[i].carikartunvani;
                        cariKartAdresler1._key_scf_carikart = calElemanObj[i]._key_scf_carikart;
                        cariKartAdresler1.adres1 = calElemanObj[i].adres1;
                        cariKartAdresler1.adres2 = calElemanObj[i].adres2;


                        _scfcontext.CariKartAdreslers.Update(cariKartAdresler1);
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
                        CariKartAdresler cariKartAdresler1 = new CariKartAdresler();
                        cariKartAdresler1._key = calElemanObj[i]._key;
                        cariKartAdresler1.adresadi = calElemanObj[i].adresadi;
                        cariKartAdresler1.carikartunvani = calElemanObj[i].carikartunvani;
                        cariKartAdresler1._key_scf_carikart = calElemanObj[i]._key_scf_carikart;
                        cariKartAdresler1.adres1 = calElemanObj[i].adres1;
                        cariKartAdresler1.adres2 = calElemanObj[i].adres2;



                        _scfcontext.CariKartAdreslers.Add(cariKartAdresler1);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (calresponse.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + calresponse.code + " Message: " + calresponse.msg);

            }
            else
            {
                Console.WriteLine("Stoklar getirilirken bir hata oluştu. Code: " + calresponse.code + " Message: " + calresponse.msg);

                return Ok();
            }
            #endregion
            #region scf_carikart_yetkili_listele
            dynamic cyresponse = Sabitler.sendMessageToServer(scf_carikart_yetkili_listele(webServisBilgi), Sabitler.scfEk);


            if (cyresponse == null)
            {
                Console.WriteLine("Stoklar getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (cyresponse.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
                Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
                Console.WriteLine(cyresponse.result + cyresponse.msg);
                dynamic cyElemanObj = cyresponse.result;
                int temp = cyElemanObj.Count;
                int i = 0;
                for (; i < temp; i++)
                {
                    string temp2 = cyElemanObj[i]._key;
                    bool flag = await _scfcontext.CariKartYetkilis.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    cariKartYetkili = await _scfcontext.CariKartYetkilis.FirstOrDefaultAsync(u => u._key == temp2);
                    if (flag)
                    {
                        CariKartYetkili cariKartYetkili1 = await _scfcontext.CariKartYetkilis.FirstOrDefaultAsync(u => u._key == temp2);
                        cariKartYetkili1._key_scf_carikart = cyElemanObj[i]._key_scf_carikart;
                        cariKartYetkili1._key_sis_rehber_karti = cyElemanObj[i]._key_sis_rehber_karti;
                        cariKartYetkili1.kodu = cyElemanObj[i].kodu;
                        cariKartYetkili1.istelno = cyElemanObj[i].istelno;
                        cariKartYetkili1.gorev = cyElemanObj[i].gorev;
                        cariKartYetkili1.ceptelno = cyElemanObj[i].ceptelno;
                        cariKartYetkili1.adsoyad = cyElemanObj[i].adsoyad;
                        cariKartYetkili1.carikartkodu = cyElemanObj[i].carikartkodu;




                        _scfcontext.CariKartYetkilis.Update(cariKartYetkili1);
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
                        CariKartYetkili cariKartYetkili1 = new CariKartYetkili();
                        cariKartYetkili1._key = cyElemanObj[i]._key;
                        cariKartYetkili1._key_scf_carikart = cyElemanObj[i]._key_scf_carikart;
                        cariKartYetkili1._key_sis_rehber_karti = cyElemanObj[i]._key_sis_rehber_karti;
                        cariKartYetkili1.kodu = cyElemanObj[i].kodu;
                        cariKartYetkili1.istelno = cyElemanObj[i].istelno;
                        cariKartYetkili1.gorev = cyElemanObj[i].gorev;
                        cariKartYetkili1.ceptelno = cyElemanObj[i].ceptelno;
                        cariKartYetkili1.adsoyad = cyElemanObj[i].adsoyad;
                        cariKartYetkili1.carikartkodu = cyElemanObj[i].carikartkodu;



                        _scfcontext.CariKartYetkilis.Add(cariKartYetkili1);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (cyresponse.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + cyresponse.code + " Message: " + cyresponse.msg);

            }
            else
            {
                Console.WriteLine("Stoklar getirilirken bir hata oluştu. Code: " + cyresponse.code + " Message: " + cyresponse.msg);

                return Ok();
            }
            #endregion
            #region sis_doviz_listele
            dynamic dresponse = Sabitler.sendMessageToServer(sis_doviz_listele(webServisBilgi), Sabitler.sisEk);


            if (dresponse == null)
            {
                Console.WriteLine("Stoklar getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (dresponse.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
                Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
                Console.WriteLine(dresponse.result + dresponse.msg);
                dynamic dElemanObj = dresponse.result;
                int temp = dElemanObj.Count;
                int i = 0;
                for (; i < temp; i++)
                {
                    string temp2 = dElemanObj[i]._key;
                    bool flag = await _scfcontext.Dovizs.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    doviz = await _scfcontext.Dovizs.FirstOrDefaultAsync(u => u._key == temp2);
                    if (flag)
                    {
                        Doviz doviz1 = await _scfcontext.Dovizs.FirstOrDefaultAsync(u => u._key == temp2);
                        //doviz._key = dElemanObj[i]._key;
                        doviz1.uzunadi = dElemanObj[i].uzunadi;
                        doviz1.adi = dElemanObj[i].adi;
                        //doviz1.aktif = dElemanObj[i].aktif;




                        _scfcontext.Dovizs.Update(doviz1);
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
                        Doviz doviz1 = new Doviz();
                        doviz1._key = dElemanObj[i]._key;
                        doviz1.uzunadi = dElemanObj[i].uzunadi;
                        doviz1.adi = dElemanObj[i].adi;
                        //doviz1.aktif = dElemanObj[i].aktif;



                        _scfcontext.Dovizs.Add(doviz1);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (dresponse.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + dresponse.code + " Message: " + dresponse.msg);

            }
            else
            {
                Console.WriteLine("Stoklar getirilirken bir hata oluştu. Code: " + dresponse.code + " Message: " + dresponse.msg);

                return Ok();
            }
            #endregion
            #region rpr_dinamik_raporparametreleri_getir
            dynamic rresponse = Sabitler.sendMessageToServer(rpr_dinamik_raporparametreleri_getir(webServisBilgi), Sabitler.rprEk);


            if (rresponse == null)
            {
                Console.WriteLine("Stoklar getirilirken, bağlantı hatası oluştu.");

                return Ok();
            }
            // sonuc başarılı, firmakodu ve donemkodu daha sonraki islemlerde kullanılmasına karşı kaydedilir
            else if (rresponse.code == "200")
            {
                // Daha sonra firma ve donem koduna hızlı erişebilmek için
                // Sabitler içerisinde firmaKodu ve donemKodu kaydedilir.

                Sabitler.seciliFirmaKodu = int.Parse(webServisBilgi.FirmaKod);
                Sabitler.seciliDonemKodu = int.Parse(webServisBilgi.DonemKod);
                Console.WriteLine(rresponse.result);

                dynamic dElemanObj = rresponse.result;
                string tasarımkeyveisimler = dElemanObj[0].alanlar[0].widgetinit;//"[[\"1125\", \"Teklif Fi\\u015fi (Grafik)\"], [\"1124\", \"Teklif Fi\\u015fi (Karakter)\"], [\"2542\", \"testo1 (Grafik)\"]]"	System.String
                var orderArray = tasarımkeyveisimler.Split(',').Select(x => x.Split('\"')).ToArray();



                int temp = dElemanObj.Count;
                int i = 0;
                for (; i < orderArray.Length; i++)
                {
                    string temp2 = orderArray[i][1];
                    bool flag = await _scfcontext.Rpr_dinamik_raporparametreleri_getir.AnyAsync(x => x._key == temp2); // if _key exists flag==true
                    //r = await _scfcontext.Dovizs.FirstOrDefaultAsync(u => u._key == temp2);
                    if (flag)
                    {
                        
                        Rpr_dinamik_raporparametreleri_getir tasarim = await _scfcontext.Rpr_dinamik_raporparametreleri_getir.FirstOrDefaultAsync(u => u._key == temp2);
                        var sontest = orderArray[i][1];
                        //tasarim._key = orderArray[k][1];

                        tasarim.isim = Decoder(orderArray[i + 1][1]); i++;// son kaldıgım yyer, teklif tasarım isimleri unicode geliyor ( \\u015f gibi) toString()kullanıcınca geçmesi gerekiyor ama az once denediiğmde geçmedi yarın burdan devam edeceğim
                        //tasarim.isim = orderArray[i + 1][1];i++;


                        _scfcontext.Rpr_dinamik_raporparametreleri_getir.Update(tasarim);
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
                        Rpr_dinamik_raporparametreleri_getir tasarim = new Rpr_dinamik_raporparametreleri_getir();
                        var sontest = orderArray[i][1];
                        tasarim._key = orderArray[i][1];

                        tasarim.isim = Decoder(orderArray[i + 1][1]); i++;

                        //tasarim.isim = orderArray[i + 1][1]; i++;
                        _scfcontext.Rpr_dinamik_raporparametreleri_getir.Add(tasarim);
                        _scfcontext.SaveChanges();
                    }
                }
                i = 0;

            }
            // Yeterli kontör bulunmadığında gelen uyarı.
            else if (dresponse.code == "406")
            {
                Console.WriteLine("Yeterli kontörünüz bulunmamaktadır. Code: " + rresponse.code + " Message: " + rresponse.msg);

            }
            else
            {
                Console.WriteLine("Stoklar getirilirken bir hata oluştu. Code: " + rresponse.code + " Message: " + rresponse.msg);

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
            Console.WriteLine(JsonConvert.SerializeObject(request));
            return request;


            Console.WriteLine(JsonConvert.SerializeObject(request));
            return request;
        }
        public ExpandoObject scf_satiselemani_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_satiselemani_listele = new ExpandoObject();
            request.scf_satiselemani_listele.session_id = Sabitler.session_id;
            request.scf_satiselemani_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_satiselemani_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject sis_doviz_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.sis_doviz_listele = new ExpandoObject();
            request.sis_doviz_listele.session_id = Sabitler.session_id;
            request.sis_doviz_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.sis_doviz_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject rpr_dinamik_raporparametreleri_getir(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.rpr_dinamik_raporparametreleri_getir = new ExpandoObject();
            request.rpr_dinamik_raporparametreleri_getir.session_id = Sabitler.session_id;
            request.rpr_dinamik_raporparametreleri_getir.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.rpr_dinamik_raporparametreleri_getir.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            request.rpr_dinamik_raporparametreleri_getir.report_code = "scf2201D";
            return request;
        }
        public ExpandoObject scf_carikart_yetkili_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_carikart_yetkili_listele = new ExpandoObject();
            request.scf_carikart_yetkili_listele.session_id = Sabitler.session_id;
            request.scf_carikart_yetkili_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_carikart_yetkili_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject scf_carikart_adresleri_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_carikart_adresleri_listele = new ExpandoObject();
            request.scf_carikart_adresleri_listele.session_id = Sabitler.session_id;
            request.scf_carikart_adresleri_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_carikart_adresleri_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject scf_odeme_plani_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_odeme_plani_listele = new ExpandoObject();
            request.scf_odeme_plani_listele.session_id = Sabitler.session_id;
            request.scf_odeme_plani_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_odeme_plani_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject scf_teklif_listele_ayrintili(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_teklif_listele_ayrintili = new ExpandoObject();
            request.scf_teklif_listele_ayrintili.session_id = Sabitler.session_id;
            request.scf_teklif_listele_ayrintili.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_teklif_listele_ayrintili.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject scf_teklif_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_teklif_listele = new ExpandoObject();
            request.scf_teklif_listele.session_id = Sabitler.session_id;
            request.scf_teklif_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_teklif_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject scf_stokkart_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_stokkart_listele = new ExpandoObject();
            request.scf_stokkart_listele.session_id = Sabitler.session_id;
            request.scf_stokkart_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_stokkart_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }
        public ExpandoObject scf_carikart_listele(WSModel webServisBilgi)
        {
            dynamic request = new ExpandoObject();
            request.scf_carikart_listele = new ExpandoObject();
            request.scf_carikart_listele.session_id = Sabitler.session_id;
            request.scf_carikart_listele.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_carikart_listele.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            return request;
        }

        private static Regex _regex = new Regex(@"\\u(?<Value>[a-zA-Z0-9]{4})", RegexOptions.Compiled);
        public string Decoder(string value)
        {
            return _regex.Replace(
                value,
                m => ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString()
            );
        }
    }
}
