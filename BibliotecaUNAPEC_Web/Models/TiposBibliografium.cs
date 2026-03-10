using System;
using System.Collections.Generic;

namespace BibliotecaUNAPEC_Web.Models;

public partial class TiposBibliografium
{
    public int IdTipoBibliografia { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
