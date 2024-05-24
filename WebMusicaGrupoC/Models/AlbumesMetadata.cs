using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [ModelMetadataType(typeof(AlbumesMetadata))]
    public partial class Albumes { }
    public class AlbumesMetadata
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? Fecha { get; set; }

        public string? Genero { get; set; }
        [Required(ErrorMessage = "Un Titulo de Album es obligatorio")]
        [StringLength(160)]
        public string? Titulo { get; set; }

        public int? GruposId { get; set; }

        [Display(Name = "Grupo")]
        public virtual Grupos? Grupos { get; set; }
    }
}
