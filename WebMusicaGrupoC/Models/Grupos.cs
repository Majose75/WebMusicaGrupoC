using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Grupos
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Albumes> Albumes { get; set; } = [];

    public virtual ICollection<ConciertosGrupos> ConciertosGrupos { get; set; } = [];

    public virtual ICollection<GruposArtistas> GruposArtistas { get; set; } = [];
}
