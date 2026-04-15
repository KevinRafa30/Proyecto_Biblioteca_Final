using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaUNAPEC_Web.Models;

public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    [Display(Name = "Nombre Completo")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La cédula es obligatoria.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "La cédula debe tener exactamente 11 dígitos.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "La cédula solo debe contener números (sin guiones ni espacios).")]
    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = null!;

    [Required(ErrorMessage = "El número de carnet es obligatorio.")]
    [StringLength(20, ErrorMessage = "El carnet no puede exceder los 20 caracteres.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "El carnet solo debe contener números.")] // BLOQUEO DE LETRAS
    [Display(Name = "No. Carnet")]
    public string NoCarnet { get; set; } = null!;

    [Required(ErrorMessage = "Debe seleccionar un tipo de persona.")]
    [StringLength(20, ErrorMessage = "El tipo de persona es demasiado largo.")]
    [Display(Name = "Tipo de Persona")]
    public string TipoPersona { get; set; } = null!;

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [Display(Name = "Estado")]
    public bool? Estado { get; set; }

    public virtual ICollection<PrestamosDevolucione> PrestamosDevoluciones { get; set; } = new List<PrestamosDevolucione>();
}
