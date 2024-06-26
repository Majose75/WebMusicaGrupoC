﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Controllers
{
    public class ArtistasController : Controller
    {
        private readonly GrupoCContext _context;

        public ArtistasController(GrupoCContext context)
        {
            _context = context;
        }

        // GET: Artistas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artistas.ToListAsync());
        }

        // Listado de Artistas cuya FNacimiento es > 1950
        public async Task<IActionResult> IndexListadoArtistasMayores()
        {
            DateOnly fecha = new(1950, 12, 31);
            
            var listado3 =
                from texto in _context.Artistas
                where  texto.FechaNac > fecha
                select texto;

            return View(await listado3.ToListAsync());

        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistas = await _context.Artistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistas == null)
            {
                return NotFound();
            }

            return View(artistas);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Genero,FechaNac")] Artistas artistas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artistas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artistas);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistas = await _context.Artistas.FindAsync(id);
            if (artistas == null)
            {
                return NotFound();
            }
            return View(artistas);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Genero,FechaNac")] Artistas artistas)
        {
            if (id != artistas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artistas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistasExists(artistas.Id))
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
            return View(artistas);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistas = await _context.Artistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artistas == null)
            {
                return NotFound();
            }

            return View(artistas);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistas = await _context.Artistas.FindAsync(id);
            if (artistas != null)
            {
                _context.Artistas.Remove(artistas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistasExists(int id)
        {
            return _context.Artistas.Any(e => e.Id == id);
        }
    }
}
