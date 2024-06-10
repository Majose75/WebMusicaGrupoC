using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.ViewModels
{
    public class GrupoAlbumesViewModel
    {
            public int Id { get; set; }
            [Display(Name = "Grupo")]
        public string? GrupoNombreAlbum { get; set; }
        [Display(Name = "Album")]
        public string? NombreAlbum { get; set; }
        [Display(Name = "Fecha Album")]
        public DateOnly? FechaAlbum { get; set;}
            [Display(Name = "Genero")]
        public string? GeneroAlbum { get; set; }
    }
}
