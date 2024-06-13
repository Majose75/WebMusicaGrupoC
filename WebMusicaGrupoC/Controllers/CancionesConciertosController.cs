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
    public class CancionesConciertosController
        (IGenericRepositorio<CancionesConciertos> context, 
         IGenericRepositorio<Canciones> contextCanciones, 
         IGenericRepositorio<Conciertos> contextConciertos) : Controller
    {
        // GET: CancionesConciertos
        public async Task<IActionResult> Index()
        {
            var elemento = await context.DameTodos();
            foreach (var item in elemento.AsParallel())
            {
                item.Canciones = await contextCanciones.DameUno(item.CancionesId);
                item.Conciertos = await contextConciertos.DameUno(item.ConciertosId);
            }
            return View(elemento);
        }

        // GET: CancionesConciertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elemento = await context.DameUno(id);
            if (elemento == null)
            {
                return NotFound();
            }

            return View(elemento);
        }

        // GET: CancionesConciertos/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id");
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "Id");
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
                await context.AgregarElemento(cancionesConciertos);
                
            }
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id", cancionesConciertos.CancionesId);
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "Id", cancionesConciertos.ConciertosId);
            return View(cancionesConciertos);
        }

        // GET: CancionesConciertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConciertos = await context.DameUno(id);
            if (cancionesConciertos == null)
            {
                return NotFound();
            }
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id", cancionesConciertos.CancionesId);
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "Id", cancionesConciertos.ConciertosId);
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
                    context.ModificarElemento(id,cancionesConciertos);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CancionesConciertosExists(cancionesConciertos.Id))
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
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id", cancionesConciertos.CancionesId);
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "Id", cancionesConciertos.ConciertosId);
            return View(cancionesConciertos);
        }

        // GET: CancionesConciertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancionesConciertos = await context.DameUno(id);
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
            var cancionesConciertos = await context.DameUno(id);
            if (cancionesConciertos != null)
            {
                await context.EliminarElemento(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CancionesConciertosExists(int id)
        {
            if (await context.DameUno((int)id) == null)
                return false;
            return true;
        }
    }
}
