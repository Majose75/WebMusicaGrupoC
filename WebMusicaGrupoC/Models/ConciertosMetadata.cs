using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models
{
    [ModelMetadataType(typeof(ConciertosMetadata))]
    public partial class Conciertos { }
    public class ConciertosMetadata
    {
        public int Id { get; set; }
        [Display(Name = "Fecha/Hora")]
        [DataType(DataType.DateTime)]
        public DateTime? Fecha { get; set; }

        public string? Genero { get; set; }

        public string? Lugar { get; set; }
        [Display(Name = "Nombre")]
        public string? Titulo { get; set; }
        [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Precio { get; set; } = 0;
    }
}
