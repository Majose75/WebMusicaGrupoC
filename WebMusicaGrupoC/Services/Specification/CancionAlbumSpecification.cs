using WebMusicaGrupoC.Models;

namespace WebMusicaGrupoC.Services.Specification
{
    public class CancionAlbumSpecification (int Id): ICancionesSpecification 
    {
        public bool IsValid(Canciones cancion)
        {
                return cancion.AlbumesId==Id;
        }
    }
}
