using System;
using System.Collections.Generic;

namespace Biblioteca_Unapec.Models;

public partial class Ciencia
{
    public int IdCiencia { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
