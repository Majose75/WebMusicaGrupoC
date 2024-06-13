using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Artistas
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Genero { get; set; }

    public DateOnly? FechaNac { get; set; }

    public virtual ICollection<GruposArtistas> GruposArtistas { get; set; } = [];
}
