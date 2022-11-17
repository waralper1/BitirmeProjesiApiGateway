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

        
    }
}
