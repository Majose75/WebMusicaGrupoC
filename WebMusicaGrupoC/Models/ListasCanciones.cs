using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class ListasCanciones
{
    public int Id { get; set; }

    public int? ListasId { get; set; }

    public int? CancionesId { get; set; }

    public virtual Canciones? Canciones { get; set; }

    public virtual Listas? Listas { get; set; }
}
