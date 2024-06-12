using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Specification
{
    public interface ICancionesSpecification
    {
        bool IsValid(Canciones cancion);
    }
}
