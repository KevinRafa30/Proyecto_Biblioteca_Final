using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaUNAPEC_Web.Models;

namespace BibliotecaUNAPEC_Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LibrosApiController : ControllerBase
    {
        private readonly BibliotecaUnapecContext _context;

        // Inyectando la base de datos igual que en los controladores normales
        public LibrosApiController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<IActionResult> ObtenerCatalogo()
        {
     
            var listaLibros = await _context.Libros
                .Include(l => l.IdAutorNavigation)
                .Select(l => new {
                    id = l.IdLibro,
                    titulo = l.Titulo,
                    isbn = l.Isbn,
                   
                    autor = l.IdAutorNavigation != null ? l.IdAutorNavigation.Nombre : "Sin Autor",
                    estado = l.Estado == true ? "Disponible" : "Prestado"
                })
                .ToListAsync();


            return Ok(listaLibros);
        }
    }
}