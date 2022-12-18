using BitirmeProjesiErp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace BitirmeProjesiErp.Controllers
{
    public class SenderController : Controller
    {
        private readonly scfContext _scfcontext;
        private readonly WSContext _context;
        public SenderController(scfContext scfcontext, WSContext context)
        {

            _context = context;
            _scfcontext = scfcontext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(string? id)
        {


            // en son kalemturunu algılamıyordu
            var teklif = await _scfcontext.Teklifs.FindAsync(id);
            var kalem = await _scfcontext.TeklifKalemis.Where(x => x._key_scf_teklif == id).ToListAsync();
            var webServisBilgi = await _context.WebServisBilgisi
                .FirstOrDefaultAsync(m => m.WSID == 1);
            Sabitler.DiaLogin(webServisBilgi);
            int kalemSayisi = kalem.Count;
            var asd = 2;

            dynamic request = new ExpandoObject();
            request.scf_teklif_ekle = new ExpandoObject();
            request.scf_teklif_ekle.session_id = Sabitler.session_id;
            request.scf_teklif_ekle.firma_kodu = int.Parse(webServisBilgi.FirmaKod);
            request.scf_teklif_ekle.donem_kodu = int.Parse(webServisBilgi.DonemKod);
            request.scf_teklif_ekle.kart = new ExpandoObject() as dynamic;
            request.scf_teklif_ekle.kart._key_sis_sube_source = new ExpandoObject() as dynamic;
            request.scf_teklif_ekle.kart._key_sis_sube_source.subekodu = webServisBilgi.Sube;
            request.scf_teklif_ekle.kart._key_sis_doviz_raporlama = new ExpandoObject() as dynamic;
            request.scf_teklif_ekle.kart._key_sis_doviz_raporlama.adi = teklif.dovizturu;
            request.scf_teklif_ekle.kart._key_sis_depo_source = new ExpandoObject() as dynamic;
            request.scf_teklif_ekle.kart._key_sis_depo_source.depokodu = webServisBilgi.Depo;
            request.scf_teklif_ekle.kart._key_sis_doviz = new ExpandoObject() as dynamic;
            request.scf_teklif_ekle.kart._key_sis_doviz.adi = teklif.dovizturu;
            request.scf_teklif_ekle.kart._key_scf_carikart = Int64.Parse(teklif._key_scf_carikart);
            //request.scf_teklif_ekle._key_miy_firsat = new ExpandoObject() as dynamic;
            //request.scf_teklif_ekle._key_miy_firsat.kodu = teklif._key_miy_firsat;
            request.scf_teklif_ekle.kart.__dinamik__7 = teklif.dinamik7;
            request.scf_teklif_ekle.kart.__dinamik__8 = teklif.dinamik8;
            request.scf_teklif_ekle.kart.__dinamik__10 = teklif.dinamik10;
            //request.scf_teklif_ekle.kart._key_prj_proje = teklif._key_prj_proje; 0 geldiğinden hata veriyor.
            request.scf_teklif_ekle.kart._key_scf_satiselemani = Int64.Parse(teklif._key_satiselemanlari);
            request.scf_teklif_ekle.kart._key_scf_carikart_adresleri = Int64.Parse(teklif._key_scf_carikart_adresleri);
            request.scf_teklif_ekle.kart._key_scf_odeme_plani = Int64.Parse(teklif._key_scf_odeme_plani);
            request.scf_teklif_ekle.kart.aciklama1 = teklif.aciklama1;
            request.scf_teklif_ekle.kart.aciklama2 = teklif.aciklama2;
            request.scf_teklif_ekle.kart.aciklama3 = teklif.aciklama3;
            // request.scf_teklif_ekle.kart.bagkuryuzde = decimal.Parse((teklif.bagkuryuzde).Replace(".", ","));
            request.scf_teklif_ekle.kart.belgeno = teklif.belgeno;
            if (teklif.dovizkuru == null)
            {
                if (teklif.dovizturu == "USD")
                {
                    request.scf_teklif_ekle.kart.dovizkuru = Sabitler.usd;
                    request.scf_teklif_ekle.kart.raporlamadovizkuru = Sabitler.usd;
                }
                else if (teklif.dovizturu == "EUR")
                {
                    request.scf_teklif_ekle.kart.dovizkuru = Sabitler.eur;
                    request.scf_teklif_ekle.kart.raporlamadovizkuru = Sabitler.eur;
                }
                else
                {
                    request.scf_teklif_ekle.kart.dovizkuru = decimal.Parse(("1.0").Replace(".", ","));
                    request.scf_teklif_ekle.kart.raporlamadovizkuru = decimal.Parse(("1.0").Replace(".", ","));
                }

            }
            else
            {
                request.scf_teklif_ekle.kart.dovizkuru = decimal.Parse((teklif.dovizkuru).Replace(".", ","));
                request.scf_teklif_ekle.kart.raporlamadovizkuru = decimal.Parse((teklif.dovizkuru).Replace(".", ","));
            }
            request.scf_teklif_ekle.kart.ekalan1 = teklif.ekalan1;
            request.scf_teklif_ekle.kart.ekalan2 = teklif.ekalan2;
            request.scf_teklif_ekle.kart.ekalan3 = teklif.ekalan3;
            request.scf_teklif_ekle.kart.ekalan4 = teklif.ekalan4;
            request.scf_teklif_ekle.kart.ekalan5 = teklif.ekalan5;
            request.scf_teklif_ekle.kart.ekalan6 = teklif.ekalan6;
            request.scf_teklif_ekle.kart.onay = teklif.onay;
            request.scf_teklif_ekle.kart.saat = (teklif.saat + ":00");
            request.scf_teklif_ekle.kart.sevkadresi1 = teklif.sevkadresi1;
            request.scf_teklif_ekle.kart.sevkadresi2 = teklif.sevkadresi2;
            request.scf_teklif_ekle.kart.sevkadresi3 = teklif.sevkadresi3;
            request.scf_teklif_ekle.kart.tarih = teklif.tarih;
            request.scf_teklif_ekle.kart.turu = Int16.Parse(teklif.turu);
            request.scf_teklif_ekle.kart.fisno = teklif.fisno;

            //request.scf_teklif_ekle.kart.sontarih = teklif.sontarih;
            //request.scf_teklif_ekle.kart.ssdfyuzde = teklif.ssdfyuzde;
            //request.scf_teklif_ekle.kart.stopajyuzde = teklif.stopajyuzde;
            //request.scf_teklif_ekle.kart.ekmaliyet = teklif.ekmaliyet;
            //request.scf_teklif_ekle.kart.ithtipi = teklif.ithtipi;
            //request.scf_teklif_ekle.kart.kdvduzenorani = teklif.kdvduzenorani;
            //request.scf_teklif_ekle.kart.kdvtebligi85 = teklif.kdvtebligi85;
            //request.scf_teklif_ekle.kart.kilitli = teklif.kilitli;
            //request.scf_teklif_ekle.kart.komisyonkdvyuzde = teklif.komisyonkdvyuzde;
            //request.scf_teklif_ekle.kart.komisyonyuzde = teklif.komisyonyuzde;
            //request.scf_teklif_ekle.kart.m_altlar = teklif.m_altlar;
            //request.scf_teklif_ekle.kart.navlun = teklif.navlun;
            //request.scf_teklif_ekle.kart.navlunkdvyuzde = teklif.navlunkdvyuzde;
            //request.scf_teklif_ekle.kart.ek1yuzde = teklif.ek1yuzde;
            //request.scf_teklif_ekle.kart.ek2yuzde = teklif.ek2yuzde;
            //request.scf_teklif_ekle.kart.ek3yuzde = teklif._key_ek3yuzdescf_carikart;
            //request.scf_teklif_ekle.kart.ek4yuzde = teklif.ek4yuzde;
            //request.scf_teklif_ekle.kart.borsayuzde = teklif.borsayuzde;
            //request.scf_teklif_ekle.kart._key_sis_ozelkod1 = teklif._key_sis_ozelkod1;
            //request.scf_teklif_ekle.kart._key_sis_ozelkod2 = teklif._key_sis_ozelkod2;
            //request.scf_teklif_ekle.kart._key_sis_seviyekodu = teklif._key_sis_seviyekodu;
            //request.scf_teklif_ekle.kart._key_scf_bagliteklif = teklif._key_scf_bagliteklif;
            //request.scf_teklif_ekle.kart._key_ith_kart_ihr = teklif._key_ith_kart_ihr;
            //request.scf_teklif_ekle.kart._key_ith_kart_ith = teklif._key_ith_kart_ith;
            #region teklifnot
            //"_key_sis_sube_source": { "subekodu": "SUBE001"},
            //"_key_sis_doviz_raporlama": { "adi": "TL"},
            //"_key_sis_depo_source": { "depokodu": "DEPO001"},
            //"_key_scf_carikart": { "carikartkodu": "0000008"},
            //"_key_miy_firsat": { "kodu": "000003"},

            //"_key_ith_kart_ihr": 0,
            //"_key_ith_kart_ith": 0,
            //"_key_prj_proje": 0,
            ////"_key_satiselemanlari": [],
            //"_key_scf_bagliteklif": 0,

            //"_key_scf_carikart_adresleri": 178718,
            //"_key_scf_odeme_plani": 0,
            //"_key_scf_satiselemani": 0,

            //"_key_sis_doviz": { "adi": "TL"},

            //"_key_sis_ozelkod1": 0,
            //"_key_sis_ozelkod2": 0,
            //"_key_sis_seviyekodu": 0,
            //"aciklama1": "WS TEST",
            //"aciklama2": "",
            //"aciklama3": "",
            //"bagkuryuzde": "0.00",
            //"belgeno": "",
            //"borsayuzde": "0.00",
            //"dovizkuru": "1.0000",
            //"ek1yuzde": "0.00",
            //"ek2yuzde": "0.00",
            //"ek3yuzde": "0.00",
            //"ek4yuzde": "0.00",
            //"ekalan1": "",
            //"ekalan2": "",
            //"ekalan3": "",
            //"ekalan4": "",
            //"ekalan5": "",
            //"ekalan6": "",
            //"ekmaliyet": "0.00",
            //"fisno": "WS00001",
            //"ithtipi": "0",
            //"kdvduzenorani": "+",
            //"kdvduzentutari": "0.00",
            //"kdvtebligi85": "f",
            //"kilitli": "f",
            //"komisyonkdvyuzde": "0.00",
            //"komisyonyuzde": "0.00",
            //"m_altlar": [],
            //"navlun": "0.00",
            //"navlunkdvyuzde": "0.00",
            //"onay": "KABUL",
            //"raporlamadovizkuru": "1.0000",
            //"saat": "11:23:23",
            //"sevkadresi1": "YILDIRIMÖNÜ MAH. ÇAMDALI SOK. NO:118",
            //"sevkadresi2": "",
            //"sevkadresi3": "Keçiören ANKARA",
            //"sontarih": "2017-04-14",
            //"ssdfyuzde": "0.00",
            //"stopajyuzde": "0.00",
            //"tarih": "2017-04-26",
            //"turu": 1
            #endregion

            request.scf_teklif_ekle.kart.m_kalemler = new dynamic[kalemSayisi];
            for (int i = 0; i < kalemSayisi; i++)
            {


                request.scf_teklif_ekle.kart.m_kalemler[i] = new ExpandoObject();
                request.scf_teklif_ekle.kart.m_kalemler[i]._key_sis_depo_source = new ExpandoObject();
                request.scf_teklif_ekle.kart.m_kalemler[i]._key_scf_kalem_birimleri = new ExpandoObject();
                request.scf_teklif_ekle.kart.m_kalemler[i]._key_scf_kalem_birimleri.birimadi = kalem[i].birimadi; //BİRİM ADİ ALANI TEKLİFKALEMİNE EKLENECEK

                request.scf_teklif_ekle.kart.m_kalemler[i]._key_sis_doviz = new ExpandoObject();
                request.scf_teklif_ekle.kart.m_kalemler[i]._key_sis_doviz.adi = kalem[i].dovizadi;//kalem[i]._key_sis_doviz; //DOVİZ ADİ ALANI TEKLİFKALEMİNE EKLENECEK

                request.scf_teklif_ekle.kart.m_kalemler[i]._key_kalemturu = Int64.Parse(kalem[i]._key_kalemturu);
                //request.scf_teklif_ekle.kart.m_kalemler[i]._key_prj_proje = kalem[i]._key_prj_proje;
                //request.scf_teklif_ekle.kart.m_kalemler[i]._key_scf_fiyatkart = kalem[i]._key_scf_fiyatkart;
                request.scf_teklif_ekle.kart.m_kalemler[i]._key_scf_odeme_plani = Int64.Parse(kalem[i]._key_scf_odeme_plani);
                request.scf_teklif_ekle.kart.m_kalemler[i]._key_scf_satiselemani = Int64.Parse(teklif._key_satiselemanlari);
                request.scf_teklif_ekle.kart.m_kalemler[i]._key_sis_depo_source.depokodu = webServisBilgi.Depo;
                request.scf_teklif_ekle.kart.m_kalemler[i].miktar = decimal.Parse((kalem[i].miktar).Replace(".", ","));
                request.scf_teklif_ekle.kart.m_kalemler[i].note = kalem[i].note;
                request.scf_teklif_ekle.kart.m_kalemler[i].note2 = kalem[i].note2;
                request.scf_teklif_ekle.kart.m_kalemler[i].birimfiyati = decimal.Parse((kalem[i].birimfiyati).Replace(".", ","));
                if (kalem[i].dovizkuru == null)
                {
                    if (kalem[i].dovizadi == "USD")
                    {
                        request.scf_teklif_ekle.kart.m_kalemler[i].dovizkuru = Sabitler.usd;

                    }
                    else if (kalem[i].dovizadi == "EUR")
                    {
                        request.scf_teklif_ekle.kart.m_kalemler[i].dovizkuru = Sabitler.eur;
                    }
                    else
                    {
                        request.scf_teklif_ekle.kart.m_kalemler[i].dovizkuru = decimal.Parse(("1.0").Replace(".", ","));
                    }

                }
                else
                {
                    request.scf_teklif_ekle.kart.m_kalemler[i].dovizkuru = decimal.Parse((kalem[i].dovizkuru).Replace(".", ","));
                }
                //request.scf_teklif_ekle.kart.m_kalemler[i].dovizkuru = decimal.Parse((kalem[i].dovizkuru).Replace(".", ","));
                request.scf_teklif_ekle.kart.m_kalemler[i].kalemturu = "MLZM";
                request.scf_teklif_ekle.kart.m_kalemler[i].anamiktar = decimal.Parse((kalem[i].miktar).Replace(".", ","));


                //request.scf_teklif_ekle.kart.m_kalemler[i]._key_sis_ozelkod = kalem[i]._key_sis_ozelkod;


                //request.scf_teklif_ekle.kart.m_kalemler[i].indirim1 = kalem[i].indirim1;
                //request.scf_teklif_ekle.kart.m_kalemler[i].indirim2 = kalem[i].indirim2;
                //request.scf_teklif_ekle.kart.m_kalemler[i].indirim3 = kalem[i].indirim3;
                //request.scf_teklif_ekle.kart.m_kalemler[i].indirim4 = kalem[i].indirim4;
                //request.scf_teklif_ekle.kart.m_kalemler[i].indirim5 = kalem[i].indirim5;

                //request.scf_teklif_ekle.kart.m_kalemler[i].kdv = kalem[i].kdv;
                //request.scf_teklif_ekle.kart.m_kalemler[i].kdvdurumu = kalem[i].kdvdurumu;
                //request.scf_teklif_ekle.kart.m_kalemler[i].kdvtevkifatorani = kalem[i].kdvtevkifatorani;

                //request.scf_teklif_ekle.kart.m_kalemler[i]._key_scf_talep_kalemi = kalem[i]._key_scf_talep_kalemi;
                //request.scf_teklif_ekle.kart.m_kalemler[i]._key_shy_servisformu_malzemehizmet = kalem[i]._key_shy_servisformu_malzemehizmet;
                //request.scf_teklif_ekle.kart.m_kalemler[i]._key_sis_depo_dest = kalem[i]._key_sis_depo_dest;

                //request.scf_teklif_ekle.kart.m_kalemler[i].onay = kalem[i].onay;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ovkdvoran = kalem[i].ovkdvoran;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ovkdvtutar = kalem[i].ovkdvtutar;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ovmanuel = kalem[i].ovmanuel;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ovorantutari = kalem[i].ovorantutari;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ovtoplamtutari = kalem[i].ovtoplamtutari;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ovtutartutari = kalem[i].ovtutartutari;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ovtutartutari2 = kalem[i].ovtutartutari2;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ozelalan1 = kalem[i].ozelalan1;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ozelalan2 = kalem[i].ozelalan2;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ozelalan3 = kalem[i].ozelalan3;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ozelalan4 = kalem[i].ozelalan4;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ozelalan5 = kalem[i].ozelalan5;
                //request.scf_teklif_ekle.kart.m_kalemler[i].ozelalanf = kalem[i].ozelalanf;
                //request.scf_teklif_ekle.kart.m_kalemler[i].rezervasyon = kalem[i].rezervasyon;
                //request.scf_teklif_ekle.kart.m_kalemler[i].sirano = kalem[i].sirano;
                //request.scf_teklif_ekle.kart.m_kalemler[i].rezervasyon = kalem[i].rezervasyon;
                //request.scf_teklif_ekle.kart.m_kalemler[i].teslimattarihi = kalem[i].teslimattarihi;

                #region notes
                //"_key_kalemturu": { "stokkartkodu": "ZZ0049"},
                //"_key_prj_proje": 0,
                //"_key_scf_fiyatkart": 0,
                //"_key_scf_kalem_birimleri": 261156,
                //"_key_scf_odeme_plani": 0,
                //"_key_scf_satiselemani": 0,
                //"_key_scf_talep_kalemi": 0,
                //"_key_shy_servisformu_malzemehizmet": 0,
                //"_key_sis_depo_dest": 0,
                //"_key_sis_depo_source": { "depokodu": "DEPO001"},
                //      "_key_sis_doviz": { "adi": "TL"},
                //      "_key_sis_ozelkod": 0,
                //      "anamiktar": "1.00",
                //      "birimfiyati": "2000.00",
                //      "dovizkuru": "1.0000",
                //      "indirim1": "10.00",
                //      "indirim2": "0.00",
                //      "indirim3": "0.00",
                //      "indirim4": "0.00",
                //      "indirim5": "0.00",
                //      "kalemturu": "MLZM",
                //      "kdv": "18.00",
                //      "kdvdurumu": "H",
                //      "kdvtevkifatorani": "0",
                //      "miktar": "1.00",
                //      "note": "",
                //      "note2": "",
                //"onay": "KABUL",
                //      "ovkdvoran": "E",
                //      "ovkdvtutar": "E",
                //      "ovmanuel": "H",
                //      "ovorantutari": "0.00",
                //      "ovtoplamtutari": "0.00",
                //      "ovtutartutari": "0.00",
                //      "ovtutartutari2": "0.00",
                //      "ozelalan1": "0.00",
                //      "ozelalan2": "0.00",
                //      "ozelalan3": "0.00",
                //      "ozelalan4": "0.00",
                //      "ozelalan5": "0.00",
                //      "ozelalanf": "",
                //      "rezervasyon": "H",
                //      "sirano": 10,
                //      "teslimattarihi": null,
                #endregion
            }
            var alper = 123;
            //teklif.aktarildi = "0";
            if (teklif.aktarildi == "0") { 
                dynamic response = Sabitler.sendMessageToServer(request, Sabitler.scfEk);
                Console.WriteLine("asdas" + response.msg);
                if (true)
                {
                    string input = response.msg;
;
                    char charFrom= '«';
                    char charTo= '»';
                        int posFrom = input.IndexOf(charFrom);
                        if (posFrom != -1) //if found char
                        {
                            int posTo = input.IndexOf(charTo, posFrom + 1);
                            if (posTo != -1) //if found char
                            {
                                teklif.diafisno= input.Substring(posFrom + 1, posTo - posFrom - 1);
                            }
                            
                    }

                        
                    
                }
                teklif.aktarildi = "1";
                teklif.diakey = response.key;
                await _scfcontext.SaveChangesAsync();

                return Redirect("~/Teklifs/Index");
            }
            else
            {
                //dynamic response = Sabitler.sendMessageToServer(request, Sabitler.scfEk);
                Console.WriteLine("Teklif daha önce aktarılmış" );
                return Redirect("~/Teklifs/Index");
            }
            //Console.WriteLine("asdas" + response.msg);
            //return Redirect("~/Teklifs/Index");

        }
    }
}
