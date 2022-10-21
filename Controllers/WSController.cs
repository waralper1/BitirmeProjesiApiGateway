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
    public class WSController : Controller
    {
        private readonly WSContext _context;

        public WSController(WSContext context)
        {
            _context = context;
        }

        // GET: WSModels
        public async Task<IActionResult> Index()
        {
              return View(await _context.WebServisBilgisi.ToListAsync());
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
                return RedirectToAction(nameof(Index));
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
            {
                _context.WebServisBilgisi.Remove(wSModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WSModelExists(int id)
        {
          return _context.WebServisBilgisi.Any(e => e.WSID == id);
        }
    }
}
