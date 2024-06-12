using Microsoft.AspNetCore.Mvc;
using WebMusicaGrupoC.Services.Repositorio;
using WebMusicaGrupoC.Services.Specification;

namespace WebMusicaGrupoC.Views.Shared.Component.Canciones
{
    public class CancionesViewComponent(IGenericRepositorio<Models.Canciones> coleccion): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var items = await coleccion.DameTodos();
            ICancionesSpecification especificacion = new CancionAlbumSpecification(Id);
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }

    }
}
