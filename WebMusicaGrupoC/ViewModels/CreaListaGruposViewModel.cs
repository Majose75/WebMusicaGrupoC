using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.ViewModels
{
    public class CreaListaGruposViewModel: ICreaListaGruposViewModel
    {

        private readonly GrupoCContext context;
        private readonly IAlbumesGrupoBuilder builder;

        public CreaListaGruposViewModel(GrupoCContext context, IAlbumesGrupoBuilder builder)
        {
            this.context=context;
            this.builder=builder;
        }

        public List<AlbumesGrupoViewModel> dameTodosGrupos()
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

            var GruposDistintos = from p in context.Grupos.ToList() group (p) by p.Nombre into g select g;
            List<AlbumesGrupoViewModel> coleccionADevolver = new();
            foreach (var _grupoDistinto in GruposDistintos)
            {
                AlbumesGrupoViewModel ElementoAPoner = new()
                {
                    //Nombre= _grupoDistinto.Key
                };
                coleccionADevolver.Add(ElementoAPoner);
            }
            return coleccionADevolver;
        }
    }

}
