using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Grupos
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Albumes> Albumes { get; set; } = new List<Albumes>();

    public virtual ICollection<ConciertosGrupos> ConciertosGrupos { get; set; } = new List<ConciertosGrupos>();

    public virtual ICollection<GruposArtistas> GruposArtistas { get; set; } = new List<GruposArtistas>();
}
