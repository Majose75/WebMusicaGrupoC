using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [ModelMetadataType(typeof(CancionesConciertosMetadata))]
    public partial class CancionesConciertos { }
    public class CancionesConciertosMetadata
    {
        public int Id { get; set; }

        public int? CancionesId { get; set; }

        public int? ConciertosId { get; set; }

        public virtual Canciones? Canciones { get; set; }

        public virtual Conciertos? Conciertos { get; set; }
    }
}
