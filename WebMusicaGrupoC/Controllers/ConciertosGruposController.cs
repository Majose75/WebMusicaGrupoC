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
    public class ConciertosGruposController : Controller
    {
        //private readonly GrupoCContext _context;
        private readonly IGenericRepositorio<ConciertosGrupos> _context;
        private readonly IGenericRepositorio<Grupos> _contextGrupos;
        private readonly IGenericRepositorio<Conciertos> _contextConciertos;

        public ConciertosGruposController(IGenericRepositorio<ConciertosGrupos> context,IGenericRepositorio<Grupos> contextGrupos, IGenericRepositorio<Conciertos> contextConciertos)
        {
            _context = context;
            _contextGrupos = contextGrupos;
            _contextConciertos= contextConciertos;
        }

        // GET: ConciertosGrupos
        public async Task<IActionResult> Index()
        {
            //var grupoCContext = _context.ConciertosGrupos.Include(c => c.Conciertos).Include(c => c.Grupos);
            //return View(await grupoCContext.ToListAsync());
            var elemento = await _context.DameTodos();
            var elementoG = await _contextGrupos.DameTodos();
            var elementoC = await _contextConciertos.DameTodos();

            foreach (var item in elemento)
            {
                item.Conciertos = await _contextConciertos.DameUno((int)item.Id);
            }
            foreach (var item in elemento)
            {
                item.Grupos = await _contextGrupos.DameUno((int)item.Id);
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

            var conciertosGrupos = await _context.DameUno((int)id);
                
            if (conciertosGrupos == null)
            {
                return NotFound();
            }

            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["ConciertosId"] = new SelectList(await _context.DameTodos(), "Id", "Titulo");
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre");
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
                _context.AgregarElemento(conciertosGrupos);
              return RedirectToAction(nameof(Index));
            }
            ViewData["ConciertosId"] = new SelectList(await _context.DameTodos(), "Id", "Titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", conciertosGrupos.GruposId);
            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await _context.DameUno((int)id);
            if (conciertosGrupos == null)
            {
                return NotFound();
            }
            ViewData["ConciertosId"] = new SelectList(await _context.DameTodos(), "Id", "Titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", conciertosGrupos.GruposId);
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
                    _context.ModificarElemento((int)id, conciertosGrupos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertosGruposExists(conciertosGrupos.Id))
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
            ViewData["ConciertosId"] = new SelectList(await _context.DameTodos(), "Id", "titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(await _context.DameTodos(), "Id", "Nombre", conciertosGrupos.GruposId);
            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await _context.DameUno((int)id);

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
            var conciertosGrupos = await _context.DameUno((int)id);
            if (conciertosGrupos != null)
            {
                await _context.EliminarElemento(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertosGruposExists(int id)
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
