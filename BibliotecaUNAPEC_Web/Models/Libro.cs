
namespace Biblioteca_Unapec.Models;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string? Titulo { get; set; }

    public string? Isbn { get; set; }

    public string? AnioPublicacion { get; set; }

    public bool? Estado { get; set; }

    public int? IdAutor { get; set; }

    public int? IdEditorial { get; set; }

    public int? IdCiencia { get; set; }

    public int? IdIdioma { get; set; }

    public virtual Autore? IdAutorNavigation { get; set; }

    public virtual Ciencia? IdCienciaNavigation { get; set; }

    public virtual Editoriale? IdEditorialNavigation { get; set; }

    public virtual Idioma? IdIdiomaNavigation { get; set; }

    public virtual ICollection<PrestamosDevolucione> PrestamosDevoluciones { get; set; } = new List<PrestamosDevolucione>();
}
