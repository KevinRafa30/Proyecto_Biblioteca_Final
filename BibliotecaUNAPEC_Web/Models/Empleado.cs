using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // NECESARIO PARA LAS VALIDACIONES

namespace BibliotecaUNAPEC_Web.Models;

public partial class Empleado
{
    [Key]
    public int IdEmpleado { get; set; }

    [Required(ErrorMessage = "El nombre del empleado es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
    [Display(Name = "Nombre Completo")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La cédula es obligatoria.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "La cédula debe tener exactamente 11 dígitos.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "La cédula solo debe contener números.")]
    [Display(Name = "Cédula")]
    public string Cedula { get; set; } = null!;

    [Required(ErrorMessage = "Debe seleccionar una tanda laboral.")]
    [StringLength(20, ErrorMessage = "La tanda laboral es demasiado larga.")]
    [Display(Name = "Tanda Laboral")]
    public string TandaLaboral { get; set; } = null!;

    [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Ingreso")]
    public DateOnly? FechaIngreso { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    [Display(Name = "Estado")]
    public bool? Estado { get; set; }

    public virtual ICollection<PrestamosDevolucione> PrestamosDevoluciones { get; set; } = new List<PrestamosDevolucione>();
}