using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaUNAPEC_Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        private readonly BibliotecaUnapecContext _context;

        public UsuariosController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            // AsNoTracking mejora el rendimiento para solo lectura
            return View(await _context.Usuarios.AsNoTracking().ToListAsync());
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Cedula,NoCarnet,TipoPersona,Estado")] Usuario usuario)
        {
            // BLOQUEO CRÍTICO: Si el modelo no es válido (ej: Nombre vacío), NO entra al try
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException?.Message.Contains("truncated") == true)
                        ModelState.AddModelError(string.Empty, "Error: Cédula (11) o Carnet (20) demasiado largos.");
                    else
                        ModelState.AddModelError(string.Empty, "Error: Verifique que la Cédula o Carnet no existan ya.");
                }
            }

            // Si llegamos aquí, mostramos los errores en la vista
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Cedula,NoCarnet,TipoPersona,Estado")] Usuario usuario)
        {
            if (id != usuario.IdUsuario) return NotFound();

            // BLOQUEO CRÍTICO: Verifica las DataAnnotations (Required, StringLength) del modelo
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario)) return NotFound();
                    else throw;
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException?.Message.Contains("truncated") == true)
                        ModelState.AddModelError(string.Empty, "Error: Verifique la longitud de los campos.");
                    else
                        ModelState.AddModelError(string.Empty, "Error al actualizar. Verifique duplicados.");
                }
            }

            // Si hay errores, devolvemos a la vista. El botón de guardar NO hará nada en la DB.
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdUsuario == id);

            if (usuario == null) return NotFound();

            // Protección contra borrado: Verificar si tiene préstamos
            bool tienePrestamos = await _context.PrestamosDevoluciones.AnyAsync(p => p.IdUsuario == id);
            if (tienePrestamos)
            {
                ViewBag.MensajeBloqueo = "No se puede eliminar: El usuario tiene préstamos registrados.";
                ViewBag.Bloqueado = true;
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            // Doble validación en el servidor por seguridad
            bool tienePrestamos = await _context.PrestamosDevoluciones.AnyAsync(p => p.IdUsuario == id);
            if (tienePrestamos)
            {
                ModelState.AddModelError(string.Empty, "BLOQUEO: El usuario tiene actividad de préstamos.");
                ViewBag.Bloqueado = true;
                return View(usuario);
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}