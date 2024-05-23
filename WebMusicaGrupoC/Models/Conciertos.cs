using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models;

public partial class Conciertos
{
    public int Id { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime? Fecha { get; set; }

    public string? Genero { get; set; }

    public string? Lugar { get; set; }

    public virtual ICollection<CancionesConciertos> CancionesConciertos { get; set; } = new List<CancionesConciertos>();

    public virtual ICollection<ConciertosGrupos> ConciertosGrupos { get; set; } = new List<ConciertosGrupos>();
}
