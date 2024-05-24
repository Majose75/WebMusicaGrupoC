using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Albumes
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Genero { get; set; }

    public string? Titulo { get; set; }

    public int? GruposId { get; set; }

    public virtual ICollection<Canciones> Canciones { get; set; } = new List<Canciones>();

    public virtual Grupos? Grupos { get; set; }
}
