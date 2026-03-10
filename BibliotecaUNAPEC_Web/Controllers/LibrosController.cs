using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaUNAPEC_Web.Models;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize]
    public class LibrosController(BibliotecaUnapecContext context) : Controller
    {
        private readonly BibliotecaUnapecContext _context = context;

        // Lista de libros
        public async Task<IActionResult> Index()
        {
            
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
        public IActionResult Create()
        {
            // SelectList para que aparezcan en los dropdowns de la vista (solo activos)
            ViewData["IdAutor"] = new SelectList(_context.Autores.Where(x => x.Estado == true), "IdAutor", "Nombre");
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales.Where(x => x.Estado == true), "IdEditorial", "Nombre");
            ViewData["IdCiencia"] = new SelectList(_context.Ciencias.Where(x => x.Estado == true), "IdCiencia", "Descripcion");
            ViewData["IdIdioma"] = new SelectList(_context.Idiomas.Where(x => x.Estado == true), "IdIdioma", "Descripcion");
            ViewData["IdTipoBibliografia"] = new SelectList(_context.TiposBibliografia.Where(x => x.Estado == true), "IdTipoBibliografia", "Descripcion");

            return View();
        }

        // POST REQUEST
        [HttpPost]
        [ValidateAntiForgeryToken] // Para ataques de suplantación de identidad (CSRF)
        // Bind para incluir TODOS los campos nuevos de la base de datos y prevenir ataques de sobrepublicación (over-posting)
        public async Task<IActionResult> Create([Bind("IdLibro,Titulo,Isbn,AnioPublicacion,Estado,IdAutor,IdEditorial,IdCiencia,IdIdioma,IdTipoBibliografia")] Libro libro)
        {
            // Revisando que el modelo sea válido según DataAnnotations antes de guardar.
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay un error , recargar las listas para que la vista no falle y mostrar los errores
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Nombre", libro.IdAutor);
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdCiencia"] = new SelectList(_context.Ciencias, "IdCiencia", "Descripcion", libro.IdCiencia);
            ViewData["IdIdioma"] = new SelectList(_context.Idiomas, "IdIdioma", "Descripcion", libro.IdIdioma);
            ViewData["IdTipoBibliografia"] = new SelectList(_context.TiposBibliografia, "IdTipoBibliografia", "Descripcion", libro.IdTipoBibliografia);

            return View(libro);
        }

        // GET REQUEST: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Nombre", libro.IdAutor);
            ViewData["IdCiencia"] = new SelectList(_context.Ciencias, "IdCiencia", "Descripcion", libro.IdCiencia);
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdIdioma"] = new SelectList(_context.Idiomas, "IdIdioma", "Descripcion", libro.IdIdioma);
            ViewData["IdTipoBibliografia"] = new SelectList(_context.TiposBibliografia, "IdTipoBibliografia", "Descripcion", libro.IdTipoBibliografia);

            return View(libro);
        }

        // POST REQUEST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLibro,Titulo,Isbn,AnioPublicacion,Estado,IdAutor,IdEditorial,IdCiencia,IdIdioma,IdTipoBibliografia")] Libro libro)
        {
            if (id != libro.IdLibro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.IdLibro))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Si falla el edit, también recargamos las listas antes de devolver la vista
            ViewData["IdAutor"] = new SelectList(_context.Autores, "IdAutor", "Nombre", libro.IdAutor);
            ViewData["IdCiencia"] = new SelectList(_context.Ciencias, "IdCiencia", "Descripcion", libro.IdCiencia);
            ViewData["IdEditorial"] = new SelectList(_context.Editoriales, "IdEditorial", "Nombre", libro.IdEditorial);
            ViewData["IdIdioma"] = new SelectList(_context.Idiomas, "IdIdioma", "Descripcion", libro.IdIdioma);
            ViewData["IdTipoBibliografia"] = new SelectList(_context.TiposBibliografia, "IdTipoBibliografia", "Descripcion", libro.IdTipoBibliografia);

            return View(libro);
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.IdLibro == id);
        }
    }
}