using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [ModelMetadataType(typeof(UsuariosMetadata))]
    public partial class Usuarios { }
    public class UsuariosMetadata
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre de usuario es obligatorio")]
        public string? Nombre { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Introduzca un email válido")]
        [Required(ErrorMessage = "La dirección de email es obligatoria")]
        public string? Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Contraseña obligatoria")]
        public string? Contraseña { get; set; }
    }
}
