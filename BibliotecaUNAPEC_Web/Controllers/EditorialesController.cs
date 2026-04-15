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
    public class EditorialesController : Controller
    {
        private readonly BibliotecaUnapecContext _context;

        public EditorialesController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        // GET: Editoriales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Editoriales.ToListAsync());
        }

        // GET: Editoriales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.IdEditorial == id);

            if (editoriale == null) return NotFound();

            return View(editoriale);
        }

        // GET: Editoriales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editoriales/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEditorial,Nombre,Pais,Estado")] Editoriale editoriale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editoriale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editoriale);
        }

        // GET: Editoriales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var editoriale = await _context.Editoriales.FindAsync(id);
            if (editoriale == null) return NotFound();

            return View(editoriale);
        }

        // POST: Editoriales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEditorial,Nombre,Pais,Estado")] Editoriale editoriale)
        {
            if (id != editoriale.IdEditorial) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editoriale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorialeExists(editoriale.IdEditorial)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editoriale);
        }

        // GET: Editoriales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var editoriale = await _context.Editoriales
                .FirstOrDefaultAsync(m => m.IdEditorial == id);

            if (editoriale == null) return NotFound();

            return View(editoriale);
        }

        // POST: Editoriales/Delete/5 - VALIDACIÓN DE DEPENDENCIAS APLICADA
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Cargamos la editorial incluyendo su colección de libros
            var editoriale = await _context.Editoriales
                .Include(e => e.Libros)
                .FirstOrDefaultAsync(e => e.IdEditorial == id);

            if (editoriale == null) return RedirectToAction(nameof(Index));

            // VALIDACIÓN: ¿Tiene libros esta editorial?
            if (editoriale.Libros != null && editoriale.Libros.Any())
            {
                ModelState.AddModelError("", $"Operación cancelada: La editorial '{editoriale.Nombre}' tiene {editoriale.Libros.Count()} libros asociados. Para borrarla, debe primero eliminar o reasignar esos libros.");
                return View(editoriale);
            }

            try
            {
                _context.Editoriales.Remove(editoriale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "No se pudo eliminar el registro debido a un error en el servidor.");
                return View(editoriale);
            }
        }

        private bool EditorialeExists(int id)
        {
            return _context.Editoriales.Any(e => e.IdEditorial == id);
        }
    }
}