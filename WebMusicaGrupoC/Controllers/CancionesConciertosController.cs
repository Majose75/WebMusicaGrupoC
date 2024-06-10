using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Controllers
{
    public class CancionesConciertosController(GrupoCContext context) : Controller
    {
        private readonly GrupoCContext _context = context;

        // GET: CancionesConciertos
        public async Task<IActionResult> Index()
        {
            var grupoCContext = _context.CancionesConciertos.Include(c => c.Canciones).Include(c => c.Conciertos);
            return View(await grupoCContext.ToListAsync());
        }

        // GET: CancionesConciertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConciertos = await _context.CancionesConciertos
                .Include(c => c.Canciones)
                .Include(c => c.Conciertos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancionesConciertos == null)
            {
                return NotFound();
            }

            return View(cancionesConciertos);
        }

        // GET: CancionesConciertos/Create
        public IActionResult Create()
        {
            ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id");
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "Id");
            return View();
        }

        // POST: CancionesConciertos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CancionesId,ConciertosId")] CancionesConciertos cancionesConciertos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cancionesConciertos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id", cancionesConciertos.CancionesId);
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "Id", cancionesConciertos.ConciertosId);
            return View(cancionesConciertos);
        }

        // GET: CancionesConciertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConciertos = await _context.CancionesConciertos.FindAsync(id);
            if (cancionesConciertos == null)
            {
                return NotFound();
            }
            ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id", cancionesConciertos.CancionesId);
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "Id", cancionesConciertos.ConciertosId);
            return View(cancionesConciertos);
        }

        // POST: CancionesConciertos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CancionesId,ConciertosId")] CancionesConciertos cancionesConciertos)
        {
            if (id != cancionesConciertos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancionesConciertos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancionesConciertosExists(cancionesConciertos.Id))
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
            ViewData["CancionesId"] = new SelectList(_context.Canciones, "Id", "Id", cancionesConciertos.CancionesId);
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "Id", cancionesConciertos.ConciertosId);
            return View(cancionesConciertos);
        }

        // GET: CancionesConciertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConciertos = await _context.CancionesConciertos
                .Include(c => c.Canciones)
                .Include(c => c.Conciertos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancionesConciertos == null)
            {
                return NotFound();
            }

            return View(cancionesConciertos);
        }

        // POST: CancionesConciertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancionesConciertos = await _context.CancionesConciertos.FindAsync(id);
            if (cancionesConciertos != null)
            {
                _context.CancionesConciertos.Remove(cancionesConciertos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancionesConciertosExists(int id)
        {
            return _context.CancionesConciertos.Any(e => e.Id == id);
        }
    }
}
