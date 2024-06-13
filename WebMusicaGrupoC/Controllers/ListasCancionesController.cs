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
    public class ListasCancionesController
        (IGenericRepositorio<ListasCanciones> context, 
            IGenericRepositorio<Canciones> contextCanciones,
            IGenericRepositorio<Listas> contextListas) : Controller
    {
        // GET: ListasCanciones
        public async Task<IActionResult> Index()
        {
            var elemento = await context.DameTodos();

            foreach (var item in elemento)
            {
                item.Canciones = await contextCanciones.DameUno(item.CancionesId);
                item.Listas = await contextListas.DameUno(item.ListasId);
            }

            return View(elemento);
        }

        // GET: ListasCanciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listasCanciones = await context.DameUno(id);
            if (listasCanciones == null)
            {
                return NotFound();
            }

            return View(listasCanciones);
        }

        // GET: ListasCanciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id");
            ViewData["ListasId"] = new SelectList(await context.DameTodos(), "Id", "Id");
            return View();
        }

        // POST: ListasCanciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ListasId,CancionesId")] ListasCanciones listasCanciones)
        {
            if (ModelState.IsValid)
            {
                await context.AgregarElemento(listasCanciones);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id", listasCanciones.CancionesId);
            ViewData["ListasId"] = new SelectList(await context.DameTodos(), "Id", "Id", listasCanciones.ListasId);
            return View(listasCanciones);
        }

        // GET: ListasCanciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listasCanciones = await context.DameUno(id);
            if (listasCanciones == null)
            {
                return NotFound();
            }
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id", listasCanciones.CancionesId);
            ViewData["ListasId"] = new SelectList(await context.DameTodos(), "Id", "Id", listasCanciones.ListasId);
            return View(listasCanciones);
        }

        // POST: ListasCanciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ListasId,CancionesId")] ListasCanciones listasCanciones)
        {
            if (id != listasCanciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.ModificarElemento(id,listasCanciones);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ListasCancionesExists(listasCanciones.Id))
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
            ViewData["CancionesId"] = new SelectList(await context.DameTodos(), "Id", "Id", listasCanciones.CancionesId);
            ViewData["ListasId"] = new SelectList(await context.DameTodos(), "Id", "Id", listasCanciones.ListasId);
            return View(listasCanciones);
        }

        // GET: ListasCanciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listasCanciones = await context.DameUno(id);
            if (listasCanciones == null)
            {
                return NotFound();
            }

            return View(listasCanciones);
        }

        // POST: ListasCanciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listasCanciones = await context.DameUno(id);
            if (listasCanciones != null)
            {
                await context.EliminarElemento(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ListasCancionesExists(int id)
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
