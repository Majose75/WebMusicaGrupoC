using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [MetadataType(typeof(ListasCancionesMetadata))]
    public partial class ListasCanciones { }
    public class ListasCancionesMetadata
    {
        public int Id { get; set; }

        public int? ListasId { get; set; }

        public int? CancionesId { get; set; }

        public virtual Canciones? Canciones { get; set; }

        public virtual Listas? Listas { get; set; }
    }
}
