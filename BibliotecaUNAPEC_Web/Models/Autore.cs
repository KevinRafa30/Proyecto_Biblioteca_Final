using System;
using System.Collections.Generic;

namespace Biblioteca_Unapec.Models;

public partial class Autore
{
    public int IdAutor { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Nacionalidad { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
