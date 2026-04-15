using BibliotecaUNAPEC_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IdiomasController : Controller
    {
        private readonly BibliotecaUnapecContext _context;

        public IdiomasController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        // GET: Idiomas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Idiomas.ToListAsync());
        }

        // GET: Idiomas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var idioma = await _context.Idiomas
                .FirstOrDefaultAsync(m => m.IdIdioma == id);

            if (idioma == null) return NotFound();

            return View(idioma);
        }

        // GET: Idiomas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Idiomas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIdioma,Descripcion,Estado")] Idioma idioma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idioma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(idioma);
        }

        // GET: Idiomas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var idioma = await _context.Idiomas.FindAsync(id);
            if (idioma == null) return NotFound();

            return View(idioma);
        }

        // POST: Idiomas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdIdioma,Descripcion,Estado")] Idioma idioma)
        {
            if (id != idioma.IdIdioma) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idioma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdiomaExists(idioma.IdIdioma)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(idioma);
        }

        // GET: Idiomas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var idioma = await _context.Idiomas
                .FirstOrDefaultAsync(m => m.IdIdioma == id);

            if (idioma == null) return NotFound();

            return View(idioma);
        }

        // POST: Idiomas/Delete/5 - VALIDACIÓN DE DEPENDENCIAS APLICADA
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Cargamos el idioma incluyendo la colección de libros asociados
            var idioma = await _context.Idiomas
                .Include(i => i.Libros)
                .FirstOrDefaultAsync(i => i.IdIdioma == id);

            if (idioma == null) return RedirectToAction(nameof(Index));

            // VALIDACIÓN: ¿Hay libros en este idioma?
            if (idioma.Libros != null && idioma.Libros.Any())
            {
                ModelState.AddModelError("", $"No se puede eliminar el idioma '{idioma.Descripcion}' porque existen {idioma.Libros.Count()} libros registrados en el catálogo con esta configuración. Debe eliminar o cambiar el idioma de esos libros primero.");
                return View(idioma);
            }

            try
            {
                _context.Idiomas.Remove(idioma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error técnico al intentar procesar la eliminación.");
                return View(idioma);
            }
        }

        private bool IdiomaExists(int id)
        {
            return _context.Idiomas.Any(e => e.IdIdioma == id);
        }
    }
}