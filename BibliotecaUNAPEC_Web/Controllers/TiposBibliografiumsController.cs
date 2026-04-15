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
    public class TiposBibliografiumsController : Controller
    {
        private readonly BibliotecaUnapecContext _context;

        public TiposBibliografiumsController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        // GET: TiposBibliografiums
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposBibliografia.ToListAsync());
        }

        // GET: TiposBibliografiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tiposBibliografium = await _context.TiposBibliografia
                .FirstOrDefaultAsync(m => m.IdTipoBibliografia == id);

            if (tiposBibliografium == null) return NotFound();

            return View(tiposBibliografium);
        }

        // GET: TiposBibliografiums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposBibliografiums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoBibliografia,Descripcion,Estado")] TiposBibliografium tiposBibliografium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposBibliografium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposBibliografium);
        }

        // GET: TiposBibliografiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tiposBibliografium = await _context.TiposBibliografia.FindAsync(id);
            if (tiposBibliografium == null) return NotFound();

            return View(tiposBibliografium);
        }

        // POST: TiposBibliografiums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoBibliografia,Descripcion,Estado")] TiposBibliografium tiposBibliografium)
        {
            if (id != tiposBibliografium.IdTipoBibliografia) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposBibliografium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposBibliografiumExists(tiposBibliografium.IdTipoBibliografia)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tiposBibliografium);
        }

        // GET: TiposBibliografiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tiposBibliografium = await _context.TiposBibliografia
                .FirstOrDefaultAsync(m => m.IdTipoBibliografia == id);

            if (tiposBibliografium == null) return NotFound();

            // Verificamos dependencias para informar preventivamente en la vista
            int cantidadLibros = await _context.Libros.CountAsync(l => l.IdTipoBibliografia == id);

            if (cantidadLibros > 0)
            {
                ViewBag.MensajeBloqueo = $"No se puede eliminar: existen {cantidadLibros} libros asociados a este formato.";
                ViewBag.Bloqueado = true;
            }

            return View(tiposBibliografium);
        }

        // POST: TiposBibliografiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposBibliografium = await _context.TiposBibliografia.FindAsync(id);

            if (tiposBibliografium == null) return NotFound();

            // LÓGICA DE PROTECCIÓN:
            // Verificamos si tiene libros vinculados
            bool tieneLibros = await _context.Libros.AnyAsync(l => l.IdTipoBibliografia == id);

            if (tieneLibros)
            {
                // Agregamos el error al ModelState para que aparezca en el asp-validation-summary
                ModelState.AddModelError(string.Empty, "Atención: No es posible eliminar este formato porque tiene libros vinculados en el catálogo.");

                // Recargamos los ViewBags necesarios para la vista de vuelta
                int cantidad = await _context.Libros.CountAsync(l => l.IdTipoBibliografia == id);
                ViewBag.MensajeBloqueo = $"No se puede eliminar: existen {cantidad} libros asociados a este formato.";
                ViewBag.Bloqueado = true;

                // DEVOLVEMOS LA VISTA (No Redirect) para que el mensaje de error persista
                return View(tiposBibliografium);
            }

            try
            {
                _context.TiposBibliografia.Remove(tiposBibliografium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado al intentar eliminar el registro.");
                return View(tiposBibliografium);
            }
        }

        private bool TiposBibliografiumExists(int id)
        {
            return _context.TiposBibliografia.Any(e => e.IdTipoBibliografia == id);
        }
    }
}