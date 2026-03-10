using BibliotecaUNAPEC_Web.Models; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize]
    public class LibrosController : Controller
    {

        private readonly BibliotecaUnapecContext _context;

        //Inyeccion en la base de datos
        public LibrosController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Todas las relaciones para que en la tabla se vea el nombre de la editorial, ciencia
            var libros = await _context.Libros
                .Include(l => l.IdAutorNavigation)
                .Include(l => l.IdEditorialNavigation)
                .Include(l => l.IdCienciaNavigation)
                .Include(l => l.IdIdiomaNavigation)
                .Include(l => l.IdTipoBibliografiaNavigation)
                .ToListAsync();

            return View(libros);
        }

        // GET REQUEST: Libros/Create
        //Lista de libros
        public IActionResult Create()
        {
            //SelectList para que aparezcan en los dropdowns de la vista
            ViewData["IdAutor"] = new SelectList(_context.Autores.Where(x => x.Estado == true), "IdAutor", "Nombre");
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales.Where(x => x.Estado == true), "IdEditorial", "Nombre");
            ViewData["IdCiencia"] = new SelectList(_context.Ciencias.Where(x => x.Estado == true), "IdCiencia", "Descripcion");
            ViewData["IdIdioma"] = new SelectList(_context.Idiomas.Where(x => x.Estado == true), "IdIdioma", "Descripcion");
            ViewData["IdTipoBibliografia"] = new SelectList(_context.TiposBibliografia.Where(x => x.Estado == true), "IdTipoBibliografia", "Descripcion");

            return View();
        }

        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Bind para incluir TODOS los campos nuevos de tu base de datos
        public async Task<IActionResult> Create([Bind("IdLibro,Titulo,Isbn,AnioPublicacion,Estado,IdAutor,IdEditorial,IdCiencia,IdIdioma,IdTipoBibliografia")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay un error, debemos recargar las listas para que la vista no falle
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Nombre", libro.IdAutor);
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdCiencia"] = new SelectList(_context.Ciencias, "IdCiencia", "Descripcion", libro.IdCiencia);
            ViewData["IdIdioma"] = new SelectList(_context.Idiomas, "IdIdioma", "Descripcion", libro.IdIdioma);
            ViewData["IdTipoBibliografia"] = new SelectList(_context.TiposBibliografia, "IdTipoBibliografia", "Descripcion", libro.IdTipoBibliografia);

            return View(libro);

        }

    }
}
