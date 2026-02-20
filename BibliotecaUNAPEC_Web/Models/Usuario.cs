

namespace BibliotecaUNAPEC_Web.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Cedula { get; set; }

    public string? NoCarnet { get; set; }

    public string? TipoPersona { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<PrestamosDevolucione> PrestamosDevoluciones { get; set; } = new List<PrestamosDevolucione>();
}
