using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [MetadataType(typeof(ListasMetadata))]
    public partial class Listas { }
    public class ListasMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public int? UsuarioId { get; set; }

        public virtual Usuarios? Usuario { get; set; }
    }
}
