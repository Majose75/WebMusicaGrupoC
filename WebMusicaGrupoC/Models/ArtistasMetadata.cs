using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [MetadataType(typeof(ArtistasMetadata))]
    public partial class Artistas { }
    public class ArtistasMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Genero { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        public DateOnly? FechaNac { get; set; }
    }
}
