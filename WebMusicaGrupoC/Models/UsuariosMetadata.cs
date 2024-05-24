using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [MetadataType(typeof(UsuariosMetadata))]
    public partial class Usuarios { }
    public class UsuariosMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Email { get; set; }

        public string? Contraseña { get; set; }
    }
}
