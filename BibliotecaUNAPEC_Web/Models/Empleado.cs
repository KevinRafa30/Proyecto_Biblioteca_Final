

namespace BibliotecaUNAPEC_Web.Models;

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
