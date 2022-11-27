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



            teklifViewModel.Rpr_dinamik_raporparametreleri_getirs = new SelectList(_scfcontext.Rpr_dinamik_raporparametreleri_getir, "_key", "isim");
            return View(teklifViewModel);
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
                Rpr_dinamik_raporparametreleri_getirs = new SelectList(_scfcontext.Rpr_dinamik_raporparametreleri_getir, "_key", "isim"),
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
            Teklif teklif = new Teklif();
            var odemeplan = await _scfcontext.OdemePlanis.FindAsync(TeklifViewModel.teklif._key_scf_odeme_plani);
            var doviz = await _scfcontext.Dovizs.FindAsync(TeklifViewModel.teklif._key_sis_doviz);
            if (ModelState.IsValid)
            {
                teklif._key_scf_carikart = TeklifViewModel.teklif._key_scf_carikart;
                teklif.fisno = TeklifViewModel.teklif.fisno;
                teklif._key_rpr_dinamik_raporparametreleri_getirs = TeklifViewModel.teklif._key_rpr_dinamik_raporparametreleri_getirs;
                teklif._key_prj_proje = TeklifViewModel.teklif._key_prj_proje;
                teklif._key_scf_carikart = TeklifViewModel.teklif._key_scf_carikart;
                teklif._key_satiselemanlari = TeklifViewModel.teklif._key_satiselemanlari;
                Int64 temp = Int64.Parse(TeklifViewModel.teklif._key_scf_carikart) + 1;
                string temp2 = temp.ToString();
                teklif._key_scf_carikart_adresleri = temp2;
                teklif._key_scf_odeme_plani = TeklifViewModel.teklif._key_scf_odeme_plani;
                teklif._key_sis_sube_source = TeklifViewModel.teklif._key_sis_sube_source;
                teklif.aciklama1 = TeklifViewModel.teklif.aciklama1;
                teklif.aciklama2 = TeklifViewModel.teklif.aciklama2;
                teklif.aciklama3 = TeklifViewModel.teklif.aciklama3;
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
                teklif.sevkadresi1 = TeklifViewModel.teklif.sevkadresi1;
                teklif.sevkadresi2 = TeklifViewModel.teklif.sevkadresi2;
                teklif.sevkadresi3 = TeklifViewModel.teklif.sevkadresi3;
                teklif.toplam = Sabitler.tekliftoplam.ToString(); Sabitler.tekliftoplam = 0;
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
            }
            return View(TeklifViewModel.teklif);
        }
        private bool TeklifExists(string id)
        {
            return _scfcontext.Teklifs.Any(e => e._key == id);
        }
    }
}
