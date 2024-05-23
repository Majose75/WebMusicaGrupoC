using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class CancionesConciertos
{
    public int Id { get; set; }

    public int? CancionesId { get; set; }

    public int? ConciertosId { get; set; }

    public virtual Canciones? Canciones { get; set; }

    public virtual Conciertos? Conciertos { get; set; }
}
