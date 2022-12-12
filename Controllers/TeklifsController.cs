using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProjesiErp.Data;
using BitirmeProjesiErp.Models;

namespace BitirmeProjesiErp.Controllers
{
    public class TeklifsController : Controller
    {
        private readonly scfContext _scfcontext;

        public TeklifsController(scfContext scfcontext)
        {
            _scfcontext = scfcontext;
        }

        // GET: Teklifs teklif viewmodelini oluşturdumki cari kodundan cari alan
        public async Task<IActionResult> Index()
        {
            var Teklifs = await _scfcontext.Teklifs
              .Include(e => e.CariKart).ToListAsync();
            TeklifViewModel teklifViewModel = new TeklifViewModel();
            teklifViewModel.teklifs = Teklifs;



            teklifViewModel.Rpr_dinamik_raporparametreleri_getirs = new SelectList(_scfcontext.rpr_dinamik_raporparametreleri_getir, "_key", "isim");
            return View(teklifViewModel);
        }
        // GET: Teklifs/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _scfcontext.Teklifs == null)
            {
                return NotFound();
            }
            var seciliteklif = await _scfcontext.Teklifs.FindAsync(id);
            Sabitler.Kurlar();
            //var teklif = await _scfcontext.Teklifs.FindAsync(id);
            var TeklifViewModel = new TeklifViewModel
            {
                teklif = await _scfcontext.Teklifs.FindAsync(id),

                //Caris = (System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>)_scfcontext.CariKarts,
                Caris = new SelectList(_scfcontext.CariKarts, "_key", "unvan"),
                Dovizs = new SelectList(_scfcontext.Dovizs, "_key", "adi"),
                Rpr_dinamik_raporparametreleri_getirs = new SelectList(_scfcontext.rpr_dinamik_raporparametreleri_getir, "_key", "isim"),
                Adress = new SelectList(_scfcontext.CariKartAdreslers, "_key", "adresadi"),
                yetkili = new SelectList(_scfcontext.CariKartYetkilis, "_key", "adsoyad"),
                SatisEleman = new SelectList(_scfcontext.SatisElemanlaris, "_key", "aciklama"),
                OdemePlanis = new SelectList(_scfcontext.OdemePlanis, "_key", "Aciklama"),
                TeklifKalemis = await _scfcontext.TeklifKalemis
              .Include(e => e.StokKart).Where(x => x._key_scf_teklif == id).ToListAsync()
                //TeklifKalemis = (System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>)_scfcontext.TeklifKalemis

            };
            //TeklifViewModel.teklif.tarih = DateTime.Today.ToString();
            TeklifViewModel.yetkili = new SelectList(_scfcontext.CariKartYetkilis
                .Where(x => x._key_scf_carikart == seciliteklif._key_scf_carikart), "_key", "adsoyad");
            TeklifViewModel.Adress = new SelectList(_scfcontext.CariKartAdreslers
                .Where(x => x._key_scf_carikart == seciliteklif._key_scf_carikart), "_key", "adresadi");
            TeklifViewModel.teklif.tarih = TeklifViewModel.teklif.tarih.ToString().Substring(0, 10);
            TeklifViewModel.teklif.saat = TeklifViewModel.teklif.saat.ToString().Substring(0, 5);
            //TeklifViewModel.teklif.sevkadresi1 = _scfcontext.CariKartAdreslers.Where()
            //teklif.TeklifKalemis = new TeklifKalemi();
            if (TeklifViewModel == null)
            {
                return NotFound();
            }

            return View(TeklifViewModel);
        }
        // POST: Teklifs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, TeklifViewModel TeklifViewModel)
        {
            Teklif teklif = await _scfcontext.Teklifs.FindAsync(id);
            var odemeplan = await _scfcontext.OdemePlanis.FindAsync(TeklifViewModel.teklif._key_scf_odeme_plani);
            var kalemler = await _scfcontext.TeklifKalemis.Where(x => x._key_scf_teklif == id).ToListAsync();
            var doviz = await _scfcontext.Dovizs.FindAsync(TeklifViewModel.teklif._key_sis_doviz);

            var sevkadresi = await _scfcontext.CariKartAdreslers.FindAsync(TeklifViewModel.teklif._key_scf_carikart_adresleri);

            //var yetkili = await _scfcontext.CariKartYetkilis.FindAsync(TeklifViewModel.teklif._key_scf_carikart);

            var cari = await _scfcontext.CariKarts.FindAsync(TeklifViewModel.teklif._key_scf_carikart);




            if (id != TeklifViewModel.teklif._key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                foreach (var item in kalemler)
                {

                    Sabitler.tekliftoplam += double.Parse(item.tutari, System.Globalization.CultureInfo.InvariantCulture);
                }

                //son kaldıgım yer burası, yareın adressleri adres adına gore otomatik getirip getirmeyeceğime karar vereceğim ahmet bey ile
                // eN son burada kaldım, tekliften önemli olan alanları bırakıp diğerlerini commet alacagım
                teklif._key_scf_carikart = TeklifViewModel.teklif._key_scf_carikart;
                teklif.fisno = TeklifViewModel.teklif.fisno;
                teklif._key_rpr_dinamik_raporparametreleri_getirs = TeklifViewModel.teklif._key_rpr_dinamik_raporparametreleri_getirs;
                //teklif._key = TeklifViewModel.teklif._key;
                teklif._key_prj_proje = TeklifViewModel.teklif._key_prj_proje;
                teklif._key_scf_carikart = TeklifViewModel.teklif._key_scf_carikart;
                teklif._key_satiselemanlari = TeklifViewModel.teklif._key_satiselemanlari;
                teklif._key_scf_carikart_adresleri = TeklifViewModel.teklif._key_scf_carikart_adresleri;
                if (TeklifViewModel.teklif._key_scf_carikart_yetkili != "0")
                {
                    teklif._key_scf_carikart_yetkili = TeklifViewModel.teklif._key_scf_carikart_yetkili;
                }
                teklif._key_scf_odeme_plani = TeklifViewModel.teklif._key_scf_odeme_plani;



                teklif._key_sis_sube_source = TeklifViewModel.teklif._key_sis_sube_source;
                //teklif.aciklama = TeklifViewModel.teklif.aciklama;
                teklif.aciklama1 = TeklifViewModel.teklif.aciklama1;
                teklif.aciklama2 = TeklifViewModel.teklif.aciklama2;
                teklif.aciklama3 = TeklifViewModel.teklif.aciklama3;
                //teklif.bagkur = TeklifViewModel.teklif.bagkur;
                //teklif.bagkurdvz = TeklifViewModel.teklif.bagkurdvz;
                //teklif.bagkuryuzde = TeklifViewModel.teklif.bagkuryuzde;
                //teklif.borsa = TeklifViewModel.teklif.borsa;
                //teklif.borsadvz = TeklifViewModel.teklif.borsadvz;
                teklif.fisno = TeklifViewModel.teklif.fisno;
                teklif.belgeno = TeklifViewModel.teklif.belgeno;

                //teklif._key_scf_carikart_adresleri = TeklifViewModel.teklif.sevkadresi3;
                teklif.ekalan1 = TeklifViewModel.teklif.ekalan1;
                teklif.ekalan2 = TeklifViewModel.teklif.ekalan2;
                teklif.ekalan3 = TeklifViewModel.teklif.ekalan3;
                teklif.ekalan4 = TeklifViewModel.teklif.ekalan4;
                teklif.ekalan5 = TeklifViewModel.teklif.ekalan5;
                teklif.ekalan6 = TeklifViewModel.teklif.ekalan6;
                teklif.raporlamadovizkuru = TeklifViewModel.teklif.raporlamadovizkuru;
                teklif.saat = TeklifViewModel.teklif.saat;
                teklif.tarih = TeklifViewModel.teklif.tarih;
                teklif.teslimattarihi = TeklifViewModel.teklif.teslimattarihi;
                teklif.tipi = TeklifViewModel.teklif.tipi;
                teklif.sevkadresi1 = sevkadresi.adres1;
                teklif.sevkadresi2 = sevkadresi.adres2;
                teklif.dinamik7 = TeklifViewModel.teklif.dinamik7;
                teklif.dinamik8 = TeklifViewModel.teklif.dinamik8;
                teklif.dinamik10 = TeklifViewModel.teklif.dinamik10;
                teklif.sevkadresi3 = TeklifViewModel.teklif.sevkadresi3;
                teklif.toplam = Sabitler.tekliftoplam.ToString(); Sabitler.tekliftoplam = 0;
                //teklif.toplamdvz = TeklifViewModel.teklif.toplamdvz;
                //teklif.toplamara = TeklifViewModel.teklif.toplamara;
                //teklif.toplamaradvz = TeklifViewModel.teklif.toplamaradvz;
                //teklif.toplamindirim = TeklifViewModel.teklif.toplamindirim;
                //teklif.toplamindirimdvz = TeklifViewModel.teklif.toplamindirimdvz;
                //teklif.toplamkdv = TeklifViewModel.teklif.toplamkdv;
                //teklif.toplamkdvdvz = TeklifViewModel.teklif.toplamkdvdvz;
                //teklif.toplamkdvtevkifati = TeklifViewModel.teklif.toplamkdvtevkifati;
                //teklif.toplamkdvtevkifatidvz = TeklifViewModel.teklif.toplamkdvtevkifatidvz;
                //teklif.toplammasraf = TeklifViewModel.teklif.toplammasraf;
                //teklif.toplammasrafdvz = TeklifViewModel.teklif.toplammasrafdvz;
                ////teklif.toplamov = TeklifViewModel.teklif.toplamov;
                //teklif.toplamovdvz = TeklifViewModel.teklif.toplamovdvz;
                teklif.turu = "1";
                teklif.onay = TeklifViewModel.teklif.onay;

                teklif._key_scf_odeme_plani = odemeplan._key;
                teklif.odemeplanikodu = odemeplan.Kodu;
                teklif.odemeplaniaciklama = odemeplan.Aciklama;
                teklif.odemeplanikodu = odemeplan.Kodu;

                teklif._key_sis_doviz = doviz._key;
                teklif._key_sis_doviz_raporlama = doviz._key;
                teklif.dovizkuru = TeklifViewModel.teklif.dovizkuru;// kur bilgisi manuel olacak
                teklif.dovizturu = doviz.adi;

                try
                {
                    _scfcontext.Update(teklif);
                    await _scfcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeklifExists(TeklifViewModel.teklif._key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(TeklifViewModel.teklif);
        }
        [HttpGet]
        public async Task<IActionResult> TeklifCreate()
        {


            var TeklifViewModel = new TeklifViewModel
            {

                teklif = new Teklif(),
                //Caris = (System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>)_scfcontext.CariKarts,
                Caris = new SelectList(_scfcontext.CariKarts, "_key", "unvan"),
                Dovizs = new SelectList(_scfcontext.Dovizs, "_key", "adi"),
                Rpr_dinamik_raporparametreleri_getirs = new SelectList(_scfcontext.rpr_dinamik_raporparametreleri_getir, "_key", "isim"),
                Adress = new SelectList(_scfcontext.CariKartAdreslers, "_key", "adresadi"),
                yetkili = new SelectList(_scfcontext.CariKartYetkilis, "_key", "adsoyad"),
                SatisEleman = new SelectList(_scfcontext.SatisElemanlaris, "_key", "aciklama"),
                OdemePlanis = new SelectList(_scfcontext.OdemePlanis, "_key", "Aciklama")
            };
            try
            {
                string? a = _scfcontext.Teklifs.OrderByDescending(x => x.fisno).FirstOrDefault().fisno;
                TeklifViewModel.teklif.fisno = (int.Parse(a) + 1).ToString();
            }
            catch (Exception ex)
            {
                TeklifViewModel.teklif.fisno = "1000";
            }


            //string? a = _scfcontext.Teklifs.OrderByDescending(x => x.fisno).FirstOrDefault().fisno;
            //TeklifViewModel.teklif.fisno = (int.Parse(a) + 1).ToString();

            DateTime Tarih = DateTime.Today;
            string Tarih_str = Tarih.ToString("yyyy-MM-dd");
            TeklifViewModel.teklif.tarih = Tarih_str;
            DateTime Saat = DateTime.Now;
            string Saat_str = Saat.ToString("HH:mm");
            TeklifViewModel.teklif.saat = Saat_str;


            return View(TeklifViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeklifCreate(TeklifViewModel TeklifViewModel)
        {
            //var id = TeklifViewModel.teklif._key;

            Teklif teklif = new Teklif();
            var odemeplan = await _scfcontext.OdemePlanis.FindAsync(TeklifViewModel.teklif._key_scf_odeme_plani);
            var doviz = await _scfcontext.Dovizs.FindAsync(TeklifViewModel.teklif._key_sis_doviz);
            var sevkadresi = await _scfcontext.CariKartAdreslers.FindAsync(TeklifViewModel.teklif.sevkadresi1);
            var cari = await _scfcontext.CariKarts.FindAsync(TeklifViewModel.teklif._key_scf_carikart);


            //if (ModelState.IsValid)
            //{

                //foreach (var item in kalemler)
                //{

                //    Sabitler.tekliftoplam += double.Parse(item.tutari, System.Globalization.CultureInfo.InvariantCulture);
                //}

                //son kaldıgım yer burası, yareın adressleri adres adına gore otomatik getirip getirmeyeceğime karar vereceğim ahmet bey ile
                // eN son burada kaldım, tekliften önemli olan alanları bırakıp diğerlerini commet alacagım
                teklif._key_scf_carikart_adresleri = TeklifViewModel.teklif.sevkadresi1;
                teklif._key_scf_carikart = TeklifViewModel.teklif._key_scf_carikart;
                teklif.fisno = TeklifViewModel.teklif.fisno;
                teklif._key_rpr_dinamik_raporparametreleri_getirs = TeklifViewModel.teklif._key_rpr_dinamik_raporparametreleri_getirs;
                //teklif._key = TeklifViewModel.teklif._key;
                teklif._key_prj_proje = TeklifViewModel.teklif._key_prj_proje;
                teklif._key_scf_carikart = TeklifViewModel.teklif._key_scf_carikart;
                teklif._key_satiselemanlari = TeklifViewModel.teklif._key_satiselemanlari;

                Int64 temp = Int64.Parse(TeklifViewModel.teklif._key_scf_carikart) + 1;

                string temp2 = temp.ToString();
                teklif._key_scf_carikart_adresleri = temp2;


                if (TeklifViewModel.teklif._key_scf_carikart_yetkili != "0")
                {
                    teklif._key_scf_carikart_yetkili = TeklifViewModel.teklif._key_scf_carikart_yetkili;
                }
                //teklif._key_scf_carikart_yetkili = TeklifViewModel._key_scf_carikart_yetkili;
                teklif._key_scf_odeme_plani = TeklifViewModel.teklif._key_scf_odeme_plani;



                teklif._key_sis_sube_source = TeklifViewModel.teklif._key_sis_sube_source;
                //teklif.aciklama = TeklifViewModel.teklif.aciklama;
                teklif.aciklama1 = TeklifViewModel.teklif.aciklama1;
                teklif.aciklama2 = TeklifViewModel.teklif.aciklama2;
                teklif.aciklama3 = TeklifViewModel.teklif.aciklama3;
                //teklif.bagkur = TeklifViewModel.teklif.bagkur;
                //teklif.bagkurdvz = TeklifViewModel.teklif.bagkurdvz;
                //teklif.bagkuryuzde = TeklifViewModel.teklif.bagkuryuzde;
                //teklif.borsa = TeklifViewModel.teklif.borsa;
                //teklif.borsadvz = TeklifViewModel.teklif.borsadvz;
                teklif.fisno = TeklifViewModel.teklif.fisno;
                teklif.belgeno = TeklifViewModel.teklif.belgeno;


                teklif.ekalan1 = TeklifViewModel.teklif.ekalan1;
                teklif.ekalan2 = TeklifViewModel.teklif.ekalan2;
                teklif.ekalan3 = TeklifViewModel.teklif.ekalan3;
                teklif.ekalan4 = TeklifViewModel.teklif.ekalan4;
                teklif.ekalan5 = TeklifViewModel.teklif.ekalan5;
                teklif.ekalan6 = TeklifViewModel.teklif.ekalan6;
                teklif.raporlamadovizkuru = TeklifViewModel.teklif.raporlamadovizkuru;
                teklif.saat = TeklifViewModel.teklif.saat;
                teklif.tarih = TeklifViewModel.teklif.tarih;
                teklif.teslimattarihi = TeklifViewModel.teklif.teslimattarihi;//Geçerlilik tarihi bu alana kayıt ediliyor.
                //teklif.tipi = "0";/*TeklifViewModel.teklif.tipi;*/
                teklif.sevkadresi1 = sevkadresi.adres1;
                teklif.sevkadresi2 = sevkadresi.adres2;
                teklif.dinamik7 = TeklifViewModel.teklif.dinamik7;
                teklif.dinamik8 = TeklifViewModel.teklif.dinamik8;
                teklif.dinamik10 = TeklifViewModel.teklif.dinamik10;

                teklif.toplam = Sabitler.tekliftoplam.ToString(); Sabitler.tekliftoplam = 0;
                //teklif.toplamdvz = TeklifViewModel.teklif.toplamdvz;
                //teklif.toplamara = TeklifViewModel.teklif.toplamara;
                //teklif.toplamaradvz = TeklifViewModel.teklif.toplamaradvz;
                //teklif.toplamindirim = TeklifViewModel.teklif.toplamindirim;
                //teklif.toplamindirimdvz = TeklifViewModel.teklif.toplamindirimdvz;
                //teklif.toplamkdv = TeklifViewModel.teklif.toplamkdv;
                //teklif.toplamkdvdvz = TeklifViewModel.teklif.toplamkdvdvz;
                //teklif.toplamkdvtevkifati = TeklifViewModel.teklif.toplamkdvtevkifati;
                //teklif.toplamkdvtevkifatidvz = TeklifViewModel.teklif.toplamkdvtevkifatidvz;
                //teklif.toplammasraf = TeklifViewModel.teklif.toplammasraf;
                //teklif.toplammasrafdvz = TeklifViewModel.teklif.toplammasrafdvz;
                ////teklif.toplamov = TeklifViewModel.teklif.toplamov;
                //teklif.toplamovdvz = TeklifViewModel.teklif.toplamovdvz;
                teklif.turu = TeklifViewModel.teklif.turu;
                teklif.onay = TeklifViewModel.teklif.onay;

                teklif._key_scf_odeme_plani = odemeplan._key;
                teklif.odemeplanikodu = odemeplan.Kodu;
                teklif.odemeplaniaciklama = odemeplan.Aciklama;
                teklif.odemeplanikodu = odemeplan.Kodu;

                teklif._key_sis_doviz = doviz._key;
                teklif._key_sis_doviz_raporlama = doviz._key;
                teklif.dovizkuru = TeklifViewModel.teklif.dovizkuru;// kur bilgisi manuel olacak
                teklif.dovizturu = doviz.adi;

                try
                {
                    _scfcontext.Add(teklif);
                    await _scfcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeklifExists(TeklifViewModel.teklif._key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(TeklifViewModel.teklif);
        }
        private bool TeklifExists(string id)
        {
            return _scfcontext.Teklifs.Any(e => e._key == id);
        }
        public JsonResult CariYetkiliGetir(string id)

        {
            var test12 = _scfcontext.CariKartYetkilis.Where(x => x._key_scf_carikart == id);
            var yetkili = new SelectList(_scfcontext.CariKartYetkilis.Where(x => x._key_scf_carikart == id), "_key", "adsoyad");
            return Json(yetkili);
        }//en son cari adresslerinide fatura adresi sevk adresi gibi sadece adres ismi seçilerek getirilecek şekilde yapacaktım. 
        public JsonResult CariAdresGetir(string id)

        {
            var test12 = _scfcontext.CariKartAdreslers.Where(x => x._key_scf_carikart == id);
            var adres = new SelectList(_scfcontext.CariKartAdreslers.Where(x => x._key_scf_carikart == id), "_key", "adresadi");
            return Json(adres);
        }//en son cari adresslerinide fatura adresi sevk adresi gibi sadece adres ismi seçilerek getirilecek şekilde yapacaktım. 
    }
}
