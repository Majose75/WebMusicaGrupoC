using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [ModelMetadataType(typeof(UsuariosMetadata))]
    public partial class Usuarios { }
    public class UsuariosMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "La dirección de email es obligatoria")]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        public string? Contraseña { get; set; }
    }
}
