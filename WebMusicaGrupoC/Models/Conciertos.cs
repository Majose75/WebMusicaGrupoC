using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Conciertos
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Genero { get; set; }

    public string? Lugar { get; set; }

    public string? Titulo { get; set; }

    public decimal? Precio { get; set; }

    public virtual ICollection<CancionesConciertos> CancionesConciertos { get; set; } = new List<CancionesConciertos>();

    public virtual ICollection<ConciertosGrupos> ConciertosGrupos { get; set; } = new List<ConciertosGrupos>();
}
