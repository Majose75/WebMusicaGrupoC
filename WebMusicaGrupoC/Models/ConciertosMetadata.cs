using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [MetadataType(typeof(ConciertosMetadata))]
    public partial class Conciertos { }
    public class ConciertosMetadata
    {
        public int Id { get; set; }
        [Display(Name = "Fecha/Hora")]
        [DataType(DataType.DateTime)]
        public DateTime? Fecha { get; set; }

        public string? Genero { get; set; }

        public string? Lugar { get; set; }
    }
}
