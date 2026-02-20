

namespace Biblioteca_Unapec.Models;

public partial class Idioma
{
    public int IdIdioma { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
