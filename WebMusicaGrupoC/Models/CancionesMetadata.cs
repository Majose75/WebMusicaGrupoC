using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [ModelMetadataType(typeof(CancionesMetadata))]
    public partial class Canciones { }
    public class CancionesMetadata
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Genero { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? Fecha { get; set; }

        public int? AlbumesId { get; set; }
        [Display(Name = "Album")]
        [Required(ErrorMessage = "Un Titulo de Album es obligatorio")]
        [StringLength(160)]
        public virtual Albumes? Albumes { get; set; }
    }
}
