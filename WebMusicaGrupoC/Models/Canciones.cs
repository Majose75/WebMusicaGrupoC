using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Canciones
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Genero { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? AlbumesId { get; set; }

    public virtual Albumes? Albumes { get; set; }

    public virtual ICollection<CancionesConciertos> CancionesConciertos { get; set; } = new List<CancionesConciertos>();

    public virtual ICollection<ListasCanciones> ListasCanciones { get; set; } = new List<ListasCanciones>();
}
