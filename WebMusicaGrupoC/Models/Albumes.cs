using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models;

public partial class Albumes
{
    public int Id { get; set; }
    [DataType(DataType.Date)]
    public DateOnly? Fecha { get; set; }

    public string? Genero { get; set; }
    [Required(ErrorMessage = "Un Titulo de Album es obligatorio")]
    [StringLength(160)]
    public string? Titulo { get; set; }
    
    public int? GruposId { get; set; }

    public virtual ICollection<Canciones> Canciones { get; set; } = new List<Canciones>();
    [Display(Name = "Grupo")]
    public virtual Grupos? Grupos { get; set; }
}
