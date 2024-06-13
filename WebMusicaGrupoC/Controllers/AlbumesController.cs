using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusicaGrupoC.Models;
using WebMusicaGrupoC.Services.Repositorio;
//using WebMusicaGrupoC.ViewModels;

namespace WebMusicaGrupoC.Controllers
{
    public class AlbumesController(IGenericRepositorio<Albumes> context, IGenericRepositorio<Grupos> contextGrupos)
        : Controller
    {
        //private readonly GrupoCContext _context;
        //private readonly ICreaListaGruposViewModel _builderlista;

        /*, ICreaListaGruposViewModel builderlista*/
        //_builderlista = builderlista;

        // GET: Albumes
        public async Task<IActionResult> Index()
        {
            var elemento = await context.DameTodos();
            
            foreach (var item in elemento)
            {
                item.Grupos = await contextGrupos.DameUno((int?)item.GruposId);
            }
            return View(elemento);

        }

        //Listado de los albumes por Grupo.
        public async Task<IActionResult> IndexListadoAlbumes()
        {
            //    var elemento = await context.DameTodos();
            //    foreach (var item in elemento)
            //    {
            //        item.Grupos = await contextGrupos.DameUno(item.GruposId);
            //    }
            //    //var grupoCContext = context.Albumes.Include(a => a.Grupos);
            //    var listado2 =
            //        from texto in context.DameTodos()
            //        in context.Grupos on texto.GruposId equals texto1.Id
            //        select new GrupoAlbumesViewModel()
            //        {
            //            NombreAlbum = texto.Titulo,
            //            GeneroAlbum = texto.Genero,
            //            FechaAlbum = texto.Fecha,
            //            GrupoNombreAlbum = texto1.Nombre
            //        };
            var elemento = await context.DameTodos();
            foreach (var item in elemento)
            {
                item.Grupos = await contextGrupos.DameUno((int?)item.GruposId);
            }
            var listado2 =
                from texto in await context.DameTodos()
                
                select texto;
            
            return View(listado2);



            //var listado3 =
            //    from texto in await context.DameTodos()
            //    where texto.FechaNac > fecha
            //    select texto;

            //return View(listado3);
        }


        // GET: Albumes/Details/5
        public async Task<IActionResult?> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes =await context.DameUno((int)id);
            if (albumes != null)
            {
                albumes.Grupos = await contextGrupos.DameUno((int?)albumes.GruposId);

                // if (albumes == null)
                //     return NotFound();
                return View(albumes);
            }

            return null;
        }

        // GET: Albumes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GruposId"] = new SelectList(await contextGrupos.DameTodos(), "Id", "Nombre");
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
                await context.AgregarElemento(albumes);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(await contextGrupos.DameTodos(), "Id", "Nombre", albumes.GruposId);
            return View(albumes);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes = await context.DameUno((int)id);
            if (albumes == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(await contextGrupos.DameTodos(), "Id", "Nombre", albumes.GruposId);
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
                     context.ModificarElemento((int) id,albumes);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AlbumesExists(albumes.Id))
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
            ViewData["GruposId"] = new SelectList(await contextGrupos.DameTodos(), "Id", "Nombre", albumes.GruposId);
            return View(albumes);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult?> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var albumes = await context.DameUno((int)id);
            if (albumes != null)
            {
                albumes.Grupos = await contextGrupos.DameUno((int?)albumes.GruposId);
                if (albumes == null)
                {
                    return NotFound();
                }

                return View(albumes);
            }

            return null;
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var albumes = await context.DameUno(id);
           
            if (albumes != null)
            {
                 await context.EliminarElemento(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AlbumesExists(int id)
        {
            if (await context.DameUno(id) == null)
                return false;
            else
                return true;
        }
    }
    
}
