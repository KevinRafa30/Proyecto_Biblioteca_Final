using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BibliotecaUNAPEC_Web.Models; 
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize]
    public class LibrosController : Controller
    {

        private readonly BibliotecaUnapecContext _context;

        //inyeccion de la base de datos
        public LibrosController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        //formulario para nuevo libro
        //get /Libros/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Para ataques de suplantación de identidad (CSRF)
        public async Task<IActionResult> Create([Bind("IdLibro,Titulo,Isbn,AnioPublicacion,IdAutor")] Libro libro) //El Bind es para especificar qué propiedades del modelo se deben enlazar desde el formulario,
                                                                                                                   //lo que ayuda a prevenir ataques de sobrepublicación (over-posting) al limitar
                                                                                                                   //los campos que pueden ser modificados por el usuario.
                                                                                                         
        {
            
            // Revisando que el ISBN y Título no estén vacíos antes de guardar.
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Hay que hacer index
            }

            // En caso de un fallo, volver a la vista para mostrar los errores
            return View(libro);
        }

        public async Task<IActionResult> Index()
        {
            // Lista de libros con sus autores incluidos
            var libros = await _context.Libros
                .Include(l => l.IdAutorNavigation)
                .ToListAsync();

            return View(libros);
        }

 
     
    }
}
