using System.ComponentModel.DataAnnotations;
namespace BibliotecaUNAPEC_Web.Models;


public partial class Libro
{
    [Key]
    public int IdLibro { get; set; }


    [Required(ErrorMessage = "El título es obligatorio")] 
    [StringLength(200)]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "El ISBN es requerido")]
    [StringLength(20, ErrorMessage = "El ISBN no puede exceder los 20 caracteres")]
    public string? Isbn { get; set; } = null!;

    [Display(Name = "Año de Publicación")]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "El año debe ser de 4 dígitos")] 
    public string? AnioPublicacion { get; set; }

    public bool? Estado { get; set; } = true;

    [Required(ErrorMessage = "Debe seleccionar un autor")]
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
