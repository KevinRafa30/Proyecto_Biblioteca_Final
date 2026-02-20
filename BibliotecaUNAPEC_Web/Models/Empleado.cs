using BibliotecaUNAPEC_Web.Models;
using System;
using System.Collections.Generic;

namespace Biblioteca_Unapec.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? Nombre { get; set; }

    public string? Cedula { get; set; }

    public string? TandaLaboral { get; set; }

    public DateOnly? FechaIngreso { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<PrestamosDevolucione> PrestamosDevoluciones { get; set; } = new List<PrestamosDevolucione>();
}
