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
    public class ListasController(IGenericRepositorio<Listas> context, IGenericRepositorio<Usuarios> contextUsuarios)
        : Controller
    {
        //private readonly GrupoCContext _context;

        // GET: Listas
        public async Task<IActionResult> Index()
        {
            var elemento = await context.DameTodos();

            foreach (var item in elemento)
            {
                item.Usuario = await contextUsuarios.DameUno(item.UsuarioId);
            }

            return View(elemento);
        }

        // GET: Listas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listas = await context.DameUno((int)id);

            if (listas == null)
            {
                return NotFound();
            }

            return View(listas);
        }

        // GET: Listas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UsuarioId"] = new SelectList(await context.DameTodos(), "Id", "Id");
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
                await context.AgregarElemento(listas);
                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioId"] = new SelectList(await context.DameTodos(), "Id", "Id", listas.UsuarioId);
            return View(listas);
        }

        // GET: Listas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listas = await context.DameUno((int)id);
            if (listas == null)
            {
                return NotFound();
            }

            ViewData["UsuarioId"] = new SelectList(await context.DameTodos(), "Id", "Id", listas.UsuarioId);
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
                    context.ModificarElemento((int)id, listas);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ListasExists(listas.Id))
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

            ViewData["UsuarioId"] = new SelectList(await context.DameTodos(), "Id", "Id", listas.UsuarioId);
            return View(listas);
        }

        // GET: Listas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listas = await context.DameUno((int)id);
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
            var listas = await context.DameUno((int)id);
            if (listas != null)
            {
                await context.EliminarElemento((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ListasExists(int id)
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
