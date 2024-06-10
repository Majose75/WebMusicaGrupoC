using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace WebMusicaGrupoC.Models
{
    public class Listado2ModelView
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        public string? Titulo { get; set; }
        public string? Grupo { get; set; }
    }
}
