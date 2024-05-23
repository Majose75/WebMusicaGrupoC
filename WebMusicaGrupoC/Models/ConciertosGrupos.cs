using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMusicaGrupoC.Models;

public partial class ConciertosGrupos
{
    public int Id { get; set; }

    public int? GruposId { get; set; }

    public int? ConciertosId { get; set; }
    [Display(Name = "Lugar del Concierto")]
    public virtual Conciertos? Conciertos { get; set; }
    [Display(Name = "Grupo que toca")]
    public virtual Grupos? Grupos { get; set; }
}
