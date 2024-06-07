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
        private readonly IGenericRepositorio<Grupos> _contextGrupos;
        private readonly IGenericRepositorio<Conciertos> _contextConciertos;

        public ConciertosGruposController(IGenericRepositorio<Grupos> contextGrupos, IGenericRepositorio<Conciertos> contextConciertos)
        {
            _contextGrupos = contextGrupos;
            _contextConciertos= contextConciertos;
        }

        // GET: ConciertosGrupos
        public async Task<IActionResult> Index()
        {
            //var grupoCContext = _context.ConciertosGrupos.Include(c => c.Conciertos).Include(c => c.Grupos);
            //return View(await grupoCContext.ToListAsync());
            var elemento = _contextConciertos.DameTodos();
            var elemento2 = _contextGrupos.DameTodos();

            foreach (var item in elemento)
            {
                item.ConciertosGrupos=_contextGrupos
            }
            //var elemento = _context.DameTodos();

            //foreach (var item in elemento)
            //{
            //    item.Grupos = _contextGrupos.DameUno((int)item.GruposId);
            //}
            //return View(elemento);
        }

        // GET: ConciertosGrupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await _context.ConciertosGrupos
                .Include(c => c.Conciertos)
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conciertosGrupos == null)
            {
                return NotFound();
            }

            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Create
        public IActionResult Create()
        {
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "Titulo");
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre");
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
                _context.Add(conciertosGrupos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "Titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", conciertosGrupos.GruposId);
            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await _context.ConciertosGrupos.FindAsync(id);
            if (conciertosGrupos == null)
            {
                return NotFound();
            }
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "Titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", conciertosGrupos.GruposId);
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
                    _context.Update(conciertosGrupos);
                    await _context.SaveChangesAsync();
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
            ViewData["ConciertosId"] = new SelectList(_context.Conciertos, "Id", "titulo", conciertosGrupos.ConciertosId);
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", conciertosGrupos.GruposId);
            return View(conciertosGrupos);
        }

        // GET: ConciertosGrupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertosGrupos = await _context.ConciertosGrupos
                .Include(c => c.Conciertos)
                .Include(c => c.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var conciertosGrupos = await _context.ConciertosGrupos.FindAsync(id);
            if (conciertosGrupos != null)
            {
                _context.ConciertosGrupos.Remove(conciertosGrupos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertosGruposExists(int id)
        {
            return _context.ConciertosGrupos.Any(e => e.Id == id);
        }
    }
}
