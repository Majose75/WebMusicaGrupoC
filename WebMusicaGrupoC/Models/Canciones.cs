using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models;

public partial class Canciones
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Genero { get; set; }
    [DataType(DataType.Date)]
    public DateOnly? Fecha { get; set; }

    public int? AlbumesId { get; set; }
    [Display(Name = "Album")]
    [Required(ErrorMessage = "Un Titulo de Album es obligatorio")]
    [StringLength(160)]
    public virtual Albumes? Albumes { get; set; }

    public virtual ICollection<CancionesConciertos> CancionesConciertos { get; set; } = new List<CancionesConciertos>();

    public virtual ICollection<ListasCanciones> ListasCanciones { get; set; } = new List<ListasCanciones>();
}
