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
    public class GruposController : Controller
    {
        //private readonly GrupoCContext _context;
        //private readonly IGruposRepositorio _repositorio;
        private readonly IGenericRepositorio<Grupos> _context;

        public GruposController(IGenericRepositorio<Grupos> context)
        {
            _context = context;
        }

        // GET: Grupos
        public async Task<IActionResult> Index()
        {
            var elemento = await _context.DameTodos();
            return View(elemento) ;
        }


        // GET: Grupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupos = await _context.DameUno((int)id);
               
            if (grupos == null)
            {
                return NotFound();
            }

            return View(grupos);
        }

        // GET: Grupos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Grupos grupos)
        {
            if (ModelState.IsValid)
            {
                await _context.AgregarElemento(grupos);
                return RedirectToAction(nameof(Index));
            }
            return View(grupos);
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupos = await _context.DameUno((int)id); 
            if (grupos == null)
            {
                return NotFound();
            }
            return View(grupos);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Grupos grupos)
        {
            if (id != grupos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ModificarElemento(grupos.Id,grupos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GruposExists(grupos.Id))
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
           
            return View(grupos);
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await _context.DameUno((int)id);
            if (grupo == null)
            {
                return NotFound();
            }

            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupo=await _context.DameUno((int)id);
            ;
            if (grupo != null)
            {
                await _context.EliminarElemento(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GruposExists(int id)
        {
            if (await _context.DameUno((int)id) == null)
                return false;
            else
            {
                return true;
            }
        }
    }
}
