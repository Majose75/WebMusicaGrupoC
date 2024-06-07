using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using WebMusicaGrupoC.Services.Repositorio;
using WebMusicaGrupoC.ViewModels;

namespace WebMusicaGrupoC.Controllers
{
    public class AlbumesController : Controller
    {
        //private readonly GrupoCContext _context;
        private readonly IGenericRepositorio<Albumes> _context;
        private readonly ICreaListaGruposViewModel _builderlista;
        private readonly IGenericRepositorio<Grupos> _contextGrupos;

        public AlbumesController(IGenericRepositorio<Albumes> context, IGenericRepositorio<Grupos> contextGrupos /*, ICreaListaGruposViewModel builderlista*/)
        {
            _context = context;
            _contextGrupos= contextGrupos;
            //_builderlista = builderlista;
        }

        // GET: Albumes
        public async Task<IActionResult> Index()
        {
            var elemento = _context.DameTodos();

            foreach (var item in elemento)
            {
                item.Grupos = _contextGrupos.DameUno((int)item.GruposId);
            }
            return View(elemento);

        }

        //Listado de los albumes por Grupo.
        //public async Task<IActionResult> IndexListadoAlbumes()
        //{

        //    var grupoCContext = _context.Albumes.Include(a => a.Grupos);
        //    var listado2 =
        //        from texto in _context.Albumes
        //        join texto1 in _context.Grupos on texto.GruposId equals texto1.Id
        //        select new GrupoAlbumesViewModel()
        //        {
        //            NombreAlbum = texto.Titulo,
        //            GeneroAlbum = texto.Genero,
        //            FechaAlbum = texto.Fecha,
        //            GrupoNombreAlbum=  texto1.Nombre
        //        };

        //    return View(await listado2.ToListAsync());
        //}


        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes = _context.DameUno((int)id);
            albumes.Grupos = _contextGrupos.DameUno((int)albumes.GruposId);
   
            if (albumes == null)
            {
                return NotFound();
            }
            return View(albumes);
        }

        // GET: Albumes/Create
        public IActionResult Create()
        {
            ViewData["GruposId"] = new SelectList(_contextGrupos.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Albumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Genero,Titulo,GruposId")] Albumes albumes)
        {
            if (ModelState.IsValid)
            {
                _context.AgregarElemento(albumes);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(_contextGrupos.DameTodos(), "Id", "Nombre", albumes.GruposId);
            return View(albumes);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes =  _context.DameUno((int)id);
            if (albumes == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(_contextGrupos.DameTodos(), "Id", "Nombre", albumes.GruposId);
            return View(albumes);
        }

        // POST: Albumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Genero,Titulo,GruposId")] Albumes albumes)
        {
            if (id != albumes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.ModificarElemento((int) id,albumes);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumesExists(albumes.Id))
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
            ViewData["GruposId"] = new SelectList(_contextGrupos.DameTodos(), "Id", "Nombre", albumes.GruposId);
            return View(albumes);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes = _context.DameUno((int)id);
            albumes.Grupos = _contextGrupos.DameUno((int)albumes.GruposId);
            if (albumes == null)
            {
                return NotFound();
            }

            return View(albumes);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albumes = _context.DameUno(id);
           
            if (albumes != null)
            {
                _context.EliminarElemento(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlbumesExists(int id)
        {
            if (_context.DameUno(id) == null)
                return false;
            else
                return true;
        }
    }
    
}
