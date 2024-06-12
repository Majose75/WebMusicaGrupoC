using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using WebMusicaGrupoC.Services.Repositorio;

namespace WebMusicaGrupoC.Controllers
{
    public class ConciertosGruposController(
        IGenericRepositorio<ConciertosGrupos> context,
        IGenericRepositorio<Grupos> contextGrupos,
        IGenericRepositorio<Conciertos> contextConciertos)
        : Controller
    {
        //private readonly GrupoCContext _context;

        // GET: ConciertosGrupos
        public async Task<IActionResult?> Index()
        {

            var elemento = await context.DameTodos();

            foreach (var item in elemento)
            {
                item.Conciertos = await contextConciertos.DameUno(item.ConciertosId);
                item.Grupos = await contextGrupos.DameUno(item.GruposId);
            }
            return View(elemento);
        }

        // GET: ConciertosGrupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await context.DameUno((int)id);

            if (conciertosGrupos == null)
            {
                return NotFound();
            }

            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "Titulo");
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: ConciertosGrupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GruposId,ConciertosId")] ConciertosGrupos conciertosGrupos)
        {
            if (ModelState.IsValid)
            {
                await context.AgregarElemento(conciertosGrupos);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "Titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Nombre", conciertosGrupos.GruposId);
            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await context.DameUno((int)id);
            if (conciertosGrupos == null)
            {
                return NotFound();
            }
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "Titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Nombre", conciertosGrupos.GruposId);
            return View(conciertosGrupos);
        }

        // POST: ConciertosGrupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GruposId,ConciertosId")] ConciertosGrupos conciertosGrupos)
        {
            if (id != conciertosGrupos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.ModificarElemento((int)id, conciertosGrupos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ConciertosGruposExists(conciertosGrupos.Id))
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
            ViewData["ConciertosId"] = new SelectList(await context.DameTodos(), "Id", "titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(await context.DameTodos(), "Id", "Nombre", conciertosGrupos.GruposId);
            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await context.DameUno((int)id);

            if (conciertosGrupos == null)
            {
                return NotFound();
            }

            return View(conciertosGrupos);
        }

        // POST: ConciertosGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conciertosGrupos = await context.DameUno((int)id);
            if (conciertosGrupos != null)
            {
                await context.EliminarElemento(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ConciertosGruposExists(int id)
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
