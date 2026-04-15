using BibliotecaUNAPEC_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AutoresController : Controller
    {
        private readonly BibliotecaUnapecContext _context;

        public AutoresController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        // GET: Autores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autores.ToListAsync());
        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autore == null)
            {
                return NotFound();
            }

            return View(autore);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAutor,Nombre,Apellido,Nacionalidad,Estado")] Autore autore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autore);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores.FindAsync(id);
            if (autore == null)
            {
                return NotFound();
            }
            return View(autore);
        }

        // POST: Autores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAutor,Nombre,Apellido,Nacionalidad,Estado")] Autore autore)
        {
            if (id != autore.IdAutor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoreExists(autore.IdAutor))
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
            return View(autore);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores
                .FirstOrDefaultAsync(m => m.IdAutor == id);
            if (autore == null)
            {
                return NotFound();
            }

            return View(autore);
        }

        // POST: Autores/Delete/5 - ACTUALIZADO CON VALIDACIÓN
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Cargamos el autor incluyendo su colección de libros para validar
            var autore = await _context.Autores
                .Include(a => a.Libros)
                .FirstOrDefaultAsync(a => a.IdAutor == id);

            if (autore == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // 1. Validar si el autor tiene libros asociados
            if (autore.Libros != null && autore.Libros.Any())
            {
                // Agregamos el error para que se muestre en el Validation Summary de la vista
                ModelState.AddModelError("", $"No se puede eliminar a '{autore.Nombre} {autore.Apellido}' porque tiene {autore.Libros.Count()} libro(s) asociados en el catálogo. Debe eliminar o reasignar los libros primero.");

                // Retornamos a la vista de confirmación con el mensaje de error
                return View(autore);
            }

            // 2. Si no tiene libros, procedemos con el borrado
            try
            {
                _context.Autores.Remove(autore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al intentar eliminar el registro de la base de datos.");
                return View(autore);
            }
        }

        private bool AutoreExists(int id)
        {
            return _context.Autores.Any(e => e.IdAutor == id);
        }
    }
}