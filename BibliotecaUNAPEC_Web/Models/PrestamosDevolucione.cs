using System;
using System.Collections.Generic;

namespace BibliotecaUNAPEC_Web.Models;

public partial class PrestamosDevolucione
{
    public int IdTransaccion { get; set; }

    public DateOnly? FechaPrestamo { get; set; }

    public DateOnly? FechaDevolucion { get; set; }

    public decimal? MontoPorDia { get; set; }

    public int? CantidadDias { get; set; }

    public string? Comentario { get; set; }

    public bool? Estado { get; set; }

    public int? IdEmpleado { get; set; }

    public int? IdLibro { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Libro? IdLibroNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
