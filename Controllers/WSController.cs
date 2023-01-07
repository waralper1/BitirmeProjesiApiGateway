using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BitirmeProjesiErp.Data;
using BitirmeProjesiErp.Models;
using Microsoft.Net.Http.Headers;
using System.Data;
using Spire.Xls;

namespace BitirmeProjesiErp.Controllers
{
    public class WSController : Controller
    {
        private readonly WSContext _context;
        private readonly FiyatlarContext _fiyatlarcontext;
        public WSController(WSContext context, FiyatlarContext fiyatlarcontext)
        {
            _context = context;
            _fiyatlarcontext = fiyatlarcontext;
        }

        // GET: WSModels
        public async Task<IActionResult> Index()
        {
            var webServisBilgi = await _context.WebServisBilgisi
                .FirstOrDefaultAsync(m => m.WSID == 1);
            //Sabitler.DiaLogin(webServisBilgi);
            
            //Console.WriteLine(Sabitler.DiaLogin(webServisBilgi));
            return View(webServisBilgi);
        }

        // GET: WSModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebServisBilgisi == null)
            {
                return NotFound();
            }

            var wSModel = await _context.WebServisBilgisi
                .FirstOrDefaultAsync(m => m.WSID == id);
            if (wSModel == null)
            {
                return NotFound();
            }

            return View(wSModel);
        }

        // GET: WSModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WSModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WSID,ApiKey,UserName,Sifre,SunucuAdi,Sube,Depo,FirmaKod,DonemKod")] WSModel wSModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wSModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wSModel);
        }

        // GET: WSModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebServisBilgisi == null)
            {
                return NotFound();
            }

            var wSModel = await _context.WebServisBilgisi.FindAsync(id);
            if (wSModel == null)
            {
                return NotFound();
            }
            return View(wSModel);
        }

        // POST: WSModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WSID,ApiKey,UserName,Sifre,SunucuAdi,Sube,Depo,FirmaKod,DonemKod")] WSModel wSModel)
        {
            if (id != wSModel.WSID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(wSModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WSModelExists(wSModel.WSID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return View(wSModel);
        }

        // GET: WSModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebServisBilgisi == null)
            {
                return NotFound();
            }

            var wSModel = await _context.WebServisBilgisi
                .FirstOrDefaultAsync(m => m.WSID == id);
            if (wSModel == null)
            {
                return NotFound();
            }

            return View(wSModel);
        }
        
        // POST: WSModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebServisBilgisi == null)
            {
                return Problem("Entity set 'WSContext.WebServisBilgisi'  is null.");
            }
            var wSModel = await _context.WebServisBilgisi.FindAsync(id);
            if (wSModel != null)
            {//a
                _context.WebServisBilgisi.Remove(wSModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WSModelExists(int id)
        {
          return _context.WebServisBilgisi.Any(e => e.WSID == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormFile"></param>
        /// <param name="tablo"></param>
        /// <returns></returns> asluda buradakileri 2. donemde datayı işlemedede kullanabilirim, yada bakıcam burdan datatable
        /// ı direk pythona atıp kullanabiliyomuyum diye
        [HttpPost]
        public async Task<IActionResult> Excell(IFormFile FormFile, int tablo)
        {
            //char temp ='"';
            //var filename = ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
            
            //get path
            var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

            //create directory "Uploads" if it doesn't exists
            if (!Directory.Exists(MainPath))
            {
                Directory.CreateDirectory(MainPath);
            }

            //get file path 
            var filePath = Path.Combine(MainPath, FormFile.FileName);
            using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
            {
                await FormFile.CopyToAsync(stream);
            }
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(filePath);
            //workbook.LoadFromFile(filename, ExcelVersion.Version97to2003);
            Worksheet sheet = workbook.Worksheets[0];

            DataTable dataSet = new DataTable();
            // get the data source that the grid is displaying data for
            dataSet = sheet.ExportDataTable();
            int cols = (dataSet.Rows.Count);

            var genislikler = dataSet.Rows[1];
            string TabloAdi = "fiyat" + tablo.ToString();
            _fiyatlarcontext.Database.ExecuteSqlRaw("TRUNCATE TABLE [" + TabloAdi + "]");//seç,li tabloya özel olacak hale getirilmeli aksi taktirde her farklı tablo aktarıldıgında fiyat1 silinecek !!!
            for (int i = 2; i < cols; i++) //kolon
            {
                DataRow row = dataSet.Rows[i];
                for (int j = 2; j < cols; j++)
                {
                    //j = 19;

                    var fiyat = row[j];
                    if (fiyat == null || fiyat == String.Empty || Convert.IsDBNull(fiyat))
                    { break; }
                    double fiyatt = Double.Parse(fiyat.ToString());

                    var genislik = genislikler[j].ToString().Replace(".", string.Empty);
                    var alper15 = genislik.GetType();
                    int genislikk = int.Parse(genislik.ToString());

                    //var uzunluk = row[1];
                    var uzunluk = row[1].ToString().Replace(".", string.Empty);
                    int uzunlukk = int.Parse(uzunluk.ToString());

                    string _key = genislik.ToString() + uzunluk.ToString();

                    FiyatT1(tablo, _key, genislikk, uzunlukk, fiyatt);
                }

            }
            _fiyatlarcontext.SaveChanges();//en son burada kaldım yarın diğer excell sayfalarını fiyat1 tablosuna aktaracağım, sonrada diğer ifleri yapcam

            return RedirectToAction("Edit", new { id = "1" });
            //return Redirect($"{Request.Path.ToString()}{Request.QueryString.Value.ToString()}");
        }
        public Task FiyatT1(int tablo, string _key, int genislik, int uzunluk, double fiyat)
        {


            try
            {


                switch (tablo)
                {
                    case 1:
                        Fiyat1 tablorow = new Fiyat1();
                        tablorow._key = _key;
                        tablorow.uzunluk = uzunluk;
                        tablorow.genislik = genislik;
                        tablorow.fiyat = fiyat;
                        Console.WriteLine("yazıldı: " + _key + "--" + fiyat);
                        _fiyatlarcontext.fiyat1.Add(tablorow);
                        break;
                    case 2:
                        Fiyat2 tablorow2 = new Fiyat2();
                        tablorow2._key = _key;
                        tablorow2.uzunluk = uzunluk;
                        tablorow2.genislik = genislik;
                        tablorow2.fiyat = fiyat;
                        Console.WriteLine("yazıldı: " + _key + "--" + fiyat);
                        _fiyatlarcontext.fiyat2.Add(tablorow2);
                        break;
                    case 3:
                        Fiyat3 tablorow3 = new Fiyat3();
                        tablorow3._key = _key;
                        tablorow3.uzunluk = uzunluk;
                        tablorow3.genislik = genislik;
                        tablorow3.fiyat = fiyat;
                        Console.WriteLine("yazıldı: " + _key + "--" + fiyat);
                        _fiyatlarcontext.fiyat3.Add(tablorow3);
                        break;
                    case 4:
                        Fiyat4 tablorow4 = new Fiyat4();
                        tablorow4._key = _key;
                        tablorow4.uzunluk = uzunluk;
                        tablorow4.genislik = genislik;
                        tablorow4.fiyat = fiyat;
                        Console.WriteLine("yazıldı: " + _key + "--" + fiyat);
                        _fiyatlarcontext.fiyat4.Add(tablorow4);
                        break;
                    case 5:
                        Fiyat5 tablorow5 = new Fiyat5();
                        tablorow5._key = _key;
                        tablorow5.uzunluk = uzunluk;
                        tablorow5.genislik = genislik;
                        tablorow5.fiyat = fiyat;
                        Console.WriteLine("yazıldı: " + _key + "--" + fiyat);
                        _fiyatlarcontext.fiyat5.Add(tablorow5);
                        break;
                    default: throw new Exception();
                }
                //_fiyatlarcontext.fiyat1.Add(tablorow);

            }
            catch (Exception ex)
            {
                //Handle Exception
            }
            return Task.CompletedTask;
        }



    
}
}
