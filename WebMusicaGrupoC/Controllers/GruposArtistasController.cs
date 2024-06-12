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
    public class GruposArtistasController(
        IGenericRepositorio<GruposArtistas> context,
        IGenericRepositorio<Grupos> contextGrupos,
        IGenericRepositorio<Artistas> contextArtistas)
        : Controller
    {
        //private readonly GrupoCContext _context;

        // GET: GruposArtistas
        public async Task<IActionResult> Index()
        {
            var elemento = await context.DameTodos();
         
            foreach (var item in elemento)
            {
                item.Artistas = await contextArtistas.DameUno(item.ArtistasId);
                item.Grupos = await contextGrupos.DameUno(item.GruposId);
            }
            
            return View(elemento);
        }

        // GET: GruposArtistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruposArtistas = await context.DameUno((int)id);

            if (gruposArtistas == null)
            {
                return NotFound();
            }

            return View(gruposArtistas);
        }

        // GET: GruposArtistas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ArtistasId"] = new SelectList(await context.DameTodos(), "Id", "Id");
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Id");
            return View();
        }

        // POST: GruposArtistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtistasId,GruposId")] GruposArtistas gruposArtistas)
        {
            if (ModelState.IsValid)
            {
                await context.AgregarElemento(gruposArtistas);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistasId"] = new SelectList(await context.DameTodos(), "Id", "Id", gruposArtistas.ArtistasId);
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Id", gruposArtistas.GruposId);
            return View(gruposArtistas);
        }

        // GET: GruposArtistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruposArtistas = await context.DameUno((int)id);
            if (gruposArtistas == null)
            {
                return NotFound();
            }
            ViewData["ArtistasId"] = new SelectList(await context.DameTodos(), "Id", "Id", gruposArtistas.ArtistasId);
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Id", gruposArtistas.GruposId);
            return View(gruposArtistas);
        }

        // POST: GruposArtistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArtistasId,GruposId")] GruposArtistas gruposArtistas)
        {
            if (id != gruposArtistas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.ModificarElemento((int)id,gruposArtistas);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GruposArtistasExists(gruposArtistas.Id))
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
            ViewData["ArtistasId"] = new SelectList(await context.DameTodos(), "Id", "Id", gruposArtistas.ArtistasId);
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Id", gruposArtistas.GruposId);
            return View(gruposArtistas);
        }

        // GET: GruposArtistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gruposArtistas = await context.DameUno((int)id);

            if (gruposArtistas == null)
            {
                return NotFound();
            }

            return View(gruposArtistas);
        }

        // POST: GruposArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gruposArtistas = await context.DameUno((int)id);
            if (gruposArtistas != null)
            {
               await context.EliminarElemento(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GruposArtistasExists(int id)
        {
            if (await context.DameUno((int)id) == null)
                return false;
            else
            {
                return true;
            }
        }
    }
}
