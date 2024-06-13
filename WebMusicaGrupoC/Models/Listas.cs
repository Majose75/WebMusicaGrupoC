using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Listas
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? UsuarioId { get; set; }

    public virtual ICollection<ListasCanciones> ListasCanciones { get; set; } = [];

    public virtual Usuarios? Usuario { get; set; }
}
