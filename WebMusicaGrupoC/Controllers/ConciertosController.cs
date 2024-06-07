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
    public class ConciertosController : Controller
    {
        //private readonly GrupoCContext _context;
        private readonly IGenericRepositorio<Conciertos> _context;

        public ConciertosController(IGenericRepositorio<Conciertos> context)
        {
            _context = context;
        }

        // GET: Conciertos
        public async Task<IActionResult> Index()
        {
            return View( _context.DameTodos());
        }
        //Listado con los conciertos cuyo Precio sea >30 y fecha posterior al 2015
        public async Task<IActionResult> IndexListadoConciertos()
        {
            DateTime fecha = new (2015,12,31);
            var listado1 =
                from texto in _context.DameTodos()
                where texto.Precio > 30 && texto.Fecha > fecha
                select texto;
            
            return View(listado1);
        }

        // GET: Conciertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = _context.DameUno((int)id);
                
            if (concierto == null)
            {
                return NotFound();
            }

            return View(concierto);
        }

        // GET: Conciertos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conciertos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Genero,Lugar,Titulo,Precio")] Conciertos concierto)
        {
            if (ModelState.IsValid)
            {
                _context.AgregarElemento(concierto);
               
                return RedirectToAction(nameof(Index));
            }
            return View(concierto);
        }

        // GET: Conciertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }
            return View(concierto);
        }

        // POST: Conciertos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Genero,Lugar,Titulo,Precio")] Conciertos concierto)
        {
            if (id != concierto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ModificarElemento((int)id,concierto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertosExists(concierto.Id))
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
            return View(concierto);
        }

        // GET: Conciertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto =  _context.DameUno((int)id);
            if (concierto == null)
            {
                return NotFound();
            }
            return View(concierto);
        }

        // POST: Conciertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conciertos = _context.DameUno((int)id);
            if (conciertos != null)
            {
                _context.EliminarElemento(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ConciertosExists(int id)
        {
            if (_context.DameUno((int)id) == null)
                return false;
            return true;
        }
    }
}
