using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models;

public partial class Artistas
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Genero { get; set; }
    [Display(Name = "Fecha Nacimiento")]
    [DataType(DataType.Date)]
    public DateOnly? FechaNac { get; set; }

    public virtual ICollection<GruposArtistas> GruposArtistas { get; set; } = new List<GruposArtistas>();
}
