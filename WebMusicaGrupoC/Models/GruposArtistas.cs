using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class GruposArtistas
{
    public int Id { get; set; }

    public int? ArtistasId { get; set; }

    public int? GruposId { get; set; }

    public virtual Artistas? Artistas { get; set; }

    public virtual Grupos? Grupos { get; set; }
}
