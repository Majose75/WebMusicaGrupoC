using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [ModelMetadataType(typeof(ConciertosGruposMetadata))]
    public partial class ConciertosGrupos { }
    public class ConciertosGruposMetadata
    {
        public int Id { get; set; }
        [Display(Name = "Grupo")]
        public int? GruposId { get; set; }
        [Display(Name = "Concierto")]
        public int? ConciertosId { get; set; }

        public virtual Conciertos? Conciertos { get; set; }

        public virtual Grupos? Grupos { get; set; }
    }
}
