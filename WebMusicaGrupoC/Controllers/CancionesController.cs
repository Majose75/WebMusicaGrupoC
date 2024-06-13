using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using WebMusicaGrupoC.Services.Repositorio;

namespace WebMusicaGrupoC.Controllers
{
    public class CancionesController(IGenericRepositorio<Canciones> context, IGenericRepositorio<Albumes> contextAlbumes) : Controller
    {
        // GET: Canciones
        public async Task<IActionResult> Index()
        {
            var elemento = await context.DameTodos();
            foreach (var item in elemento)
            {
                item.Albumes = await contextAlbumes.DameUno(item.AlbumesId);
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

            var canciones = await context.DameUno(id);
            
            if (canciones == null)
            {
                return NotFound();
            }
            else
            {
                canciones.Albumes = await contextAlbumes.DameUno(canciones.AlbumesId);
            }

            return View(canciones);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AlbumesId"] = new SelectList(await context.DameTodos(), "Id", "Titulo");
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
                await context.AgregarElemento(canciones);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(await contextAlbumes.DameTodos(), "Id", "Titulo", canciones.AlbumesId);
            return View(canciones);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await context.DameUno((int)id);
            if (canciones == null)
            {
                return NotFound();
            }
            ViewData["AlbumesId"] = new SelectList(await contextAlbumes.DameTodos(), "Id", "Titulo", canciones.AlbumesId);
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
                    context.ModificarElemento((int)id,canciones);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CancionesExists(canciones.Id))
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
            ViewData["AlbumesId"] = new SelectList(await contextAlbumes.DameTodos(), "Id", "Titulo", canciones.AlbumesId);
            return View(canciones);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult?> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await context.DameUno((int)id);
            if (canciones != null)
            {
                canciones.Albumes = await contextAlbumes.DameUno((int?)canciones.AlbumesId);
                if (canciones == null)
                {
                    return NotFound();
                }

                return View(canciones);
            }

            return null;
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var canciones = await context.DameUno(id);
            if (canciones != null)
            {
                await context.EliminarElemento(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CancionesExists(int id)
        {
            if (await context.DameUno(id) == null)
                return false;
            else
                return true;
        }
    }
}
