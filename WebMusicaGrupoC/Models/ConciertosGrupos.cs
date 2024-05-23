using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class ConciertosGrupos
{
    public int Id { get; set; }

    public int? GruposId { get; set; }

    public int? ConciertosId { get; set; }

    public virtual Conciertos? Conciertos { get; set; }

    public virtual Grupos? Grupos { get; set; }
}
