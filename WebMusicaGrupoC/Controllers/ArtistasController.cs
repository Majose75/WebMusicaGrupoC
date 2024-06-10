using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using WebMusicaGrupoC.Services.Repositorio;
using WebMusicaGrupoC.ViewModels;

namespace WebMusicaGrupoC.Controllers
{
    public class ArtistasController : Controller
    {
        private readonly IGenericRepositorio<Artistas> _context;
       //private readonly IGenericRepositorio<Grupos> _contextGrupos;

        public ArtistasController(IGenericRepositorio<Artistas> context)
        {
            _context = context;
        }

        // GET: Artistas
        public async Task<IActionResult> Index()
        {
            return View( await _context.DameTodos());
        }

        // Listado de Artistas cuya FNacimiento es > 1950
        public async Task<IActionResult> IndexListadoArtistasMayores()
        {
            DateOnly fecha = new(1950, 12, 31);
            
            var listado3 =
                from texto in await _context.DameTodos()
                where  texto.FechaNac > fecha
                select texto;

            return View(listado3);

        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistas = await _context.DameUno((int)id);
                
            if (artistas == null)
            {
                return NotFound();
            }

            return View(artistas);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Genero,FechaNac")] Artistas artistas)
        {
            if (ModelState.IsValid)
            {
                await _context.AgregarElemento(artistas);
                return RedirectToAction(nameof(Index));
            }
            return View(artistas);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistas = await _context.DameUno((int)id);
            if (artistas == null)
            {
                return NotFound();
            }
            return View(artistas);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Genero,FechaNac")] Artistas artistas)
        {
            if (id != artistas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ModificarElemento((int)id, artistas);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ArtistasExists(artistas.Id))
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
            return View(artistas);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistas = await _context.DameUno((int)id);
                
            if (artistas == null)
            {
                return NotFound();
            }
            return View(artistas);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artistas = await _context.DameUno((int)id);
            if (artistas != null)
            {
                await _context.EliminarElemento((int)id);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArtistasExists(int id)
        {
            if (await _context.DameUno(id) == null)
                return false;
            else
                return true;
        }
    }
}
