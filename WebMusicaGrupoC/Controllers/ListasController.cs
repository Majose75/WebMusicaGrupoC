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
    public class ListasController : Controller
    {
        private readonly GrupoCContext _context;

        public ListasController(GrupoCContext context)
        {
            _context = context;
        }

        // GET: Listas
        public async Task<IActionResult> Index()
        {
            var grupoCContext = _context.Listas.Include(l => l.Usuario);
            return View(await grupoCContext.ToListAsync());
        }

        // GET: Listas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listas = await _context.Listas
                .Include(l => l.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listas == null)
            {
                return NotFound();
            }

            return View(listas);
        }

        // GET: Listas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Listas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,UsuarioId")] Listas listas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", listas.UsuarioId);
            return View(listas);
        }

        // GET: Listas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listas = await _context.Listas.FindAsync(id);
            if (listas == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", listas.UsuarioId);
            return View(listas);
        }

        // POST: Listas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,UsuarioId")] Listas listas)
        {
            if (id != listas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListasExists(listas.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", listas.UsuarioId);
            return View(listas);
        }

        // GET: Listas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listas = await _context.Listas
                .Include(l => l.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listas == null)
            {
                return NotFound();
            }

            return View(listas);
        }

        // POST: Listas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listas = await _context.Listas.FindAsync(id);
            if (listas != null)
            {
                _context.Listas.Remove(listas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListasExists(int id)
        {
            return _context.Listas.Any(e => e.Id == id);
        }
    }
}
