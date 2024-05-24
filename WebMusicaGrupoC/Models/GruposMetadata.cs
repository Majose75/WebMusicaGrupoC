using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [MetadataType(typeof(GruposMetadata))]
    public partial class Grupos { }
    public class GruposMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }
    }
}
