using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using WebMusicaGrupoC.Services.Repositorio;

namespace WebMusicaGrupoC.Controllers
{
    public class CancionesController : Controller
    {
        private readonly IGenericRepositorio<Canciones> _context;
        private readonly IGenericRepositorio<Albumes> _contextAlbumes;

        public CancionesController(IGenericRepositorio<Canciones> context, IGenericRepositorio<Albumes> contextAlbumes)
        {
            _context = context;
            _contextAlbumes= contextAlbumes;
        }

        // GET: Canciones
        public async Task<IActionResult> Index()
        {
            var elemento = await _context.DameTodos();
            foreach (var item in elemento)
            {
                item.Albumes = await _contextAlbumes.DameUno((int)item.AlbumesId);
            }
            return View( elemento);
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _context.DameUno((int)id);
            canciones.Albumes = await _contextAlbumes.DameUno((int)canciones.AlbumesId);
            if (canciones == null)
            {
                return NotFound();
            }

            return View(canciones);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AlbumesId"] = new SelectList(await _context.DameTodos(), "Id", "Titulo");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Genero,Fecha,AlbumesId")] Canciones canciones)
        {
            if (ModelState.IsValid)
            {
                await _context.AgregarElemento(canciones);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(await _contextAlbumes.DameTodos(), "Id", "Titulo", canciones.AlbumesId);
            return View(canciones);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _context.DameUno((int)id);
            if (canciones == null)
            {
                return NotFound();
            }
            ViewData["AlbumesId"] = new SelectList(await _contextAlbumes.DameTodos(), "Id", "Titulo", canciones.AlbumesId);
            return View(canciones);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Genero,Fecha,AlbumesId")] Canciones canciones)
        {
            if (id != canciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ModificarElemento((int)id,canciones);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancionesExists(canciones.Id))
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
            ViewData["AlbumesId"] = new SelectList(await _contextAlbumes.DameTodos(), "Id", "Titulo", canciones.AlbumesId);
            return View(canciones);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _context.DameUno((int)id);
            canciones.Albumes = await _contextAlbumes.DameUno((int)canciones.AlbumesId);
            if (canciones == null)
            {
                return NotFound();
            }
            return View(canciones);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var canciones = await _context.DameUno(id);
            if (canciones != null)
            {
                await _context.EliminarElemento(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CancionesExists(int id)
        {
            if (_context.DameUno((int)id) == null)
                return false;
            else
            {
                return true;
            }
        }
    }
}
