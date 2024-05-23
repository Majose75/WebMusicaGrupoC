using System;
using System.Collections.Generic;

namespace WebMusicaGrupoC.Models;

public partial class Usuarios
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Email { get; set; }

    public string? Contraseña { get; set; }

    public virtual ICollection<Listas> Listas { get; set; } = new List<Listas>();
}
