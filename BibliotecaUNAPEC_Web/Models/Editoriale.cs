using System;
using System.Collections.Generic;

namespace Biblioteca_Unapec.Models;

public partial class Editoriale
{
    public int IdEditorial { get; set; }

    public string? Nombre { get; set; }

    public string? Pais { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
