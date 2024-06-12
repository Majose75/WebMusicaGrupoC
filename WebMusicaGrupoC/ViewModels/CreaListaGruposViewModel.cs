using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.ViewModels
{
    public class CreaListaGruposViewModel(GrupoCContext context)
        : ICreaListaGruposViewModel
    {
        //private readonly IAlbumesGrupoBuilder builder = builder;

        public List<AlbumesGrupoViewModel> DameTodosGrupos()
        {
            //var ProductosDistintos = from p in context.Products.ToList()
            //    group (p) by p.Color into g
            //    select g;

            //List<ProductoPorColorViewModel> coleccionADevolver = new();
            //foreach (var _colorDistinto in ProductosDistintos)
            //{
            //    ProductoPorColorViewModel ElementoAPoner =
            //        new()
            //        {
            //            Color = _colorDistinto.Key,
            //            VentasDeProducto =
            //                new ProductoPorColor01(this.context).
            //                    DamePorColor(_colorDistinto.Key).ToList()
            //        };
            //    coleccionADevolver.Add(ElementoAPoner);
            //}
            //return coleccionADevolver;

            var gruposDistintos = from p in context.Grupos.ToList() group (p) by p.Nombre into g select g;
            List<AlbumesGrupoViewModel> coleccionADevolver = [];
            foreach (var grupoDistinto in gruposDistintos)
            {
                AlbumesGrupoViewModel elementoAPoner = new()
                {
                    //Nombre= _grupoDistinto.Key
                };
                coleccionADevolver.Add(elementoAPoner);
            }
            return coleccionADevolver;
        }
    }

}
