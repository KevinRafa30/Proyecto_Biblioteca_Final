using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaUNAPEC_Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize]
    public class PrestamosDevolucionesController : Controller
    {
        private readonly BibliotecaUnapecContext _context;

        public PrestamosDevolucionesController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        // GET: PrestamosDevoluciones
        public async Task<IActionResult> Index()
        {
            var transacciones = await _context.PrestamosDevoluciones
                .Include(p => p.IdEmpleadoNavigation)
                .Include(p => p.IdLibroNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .ToListAsync();
            return View(transacciones);
        }

        // GET: PrestamosDevoluciones/Create
        public IActionResult Create()
        {
            // Solo mostrar libros cuyo Estado sea true (Disponibles)
            ViewData["IdLibro"] = new SelectList(_context.Libros.Where(l => l.Estado == true), "IdLibro", "Titulo");

            // Solo mostrar usuarios y empleados activos
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(u => u.Estado == true), "IdUsuario", "Nombre");
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados.Where(e => e.Estado == true), "IdEmpleado", "Nombre");

            return View();
        }

        // POST: PrestamosDevoluciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPrestamo,IdEmpleado,IdLibro,IdUsuario,FechaPrestamo,FechaDevolucion,MontoPorDia,CantidadDias")] PrestamosDevolucione prestamo)
        {
           
            ModelState.Remove("FechaPrestamo");

            if (ModelState.IsValid)
            {
                // 1. Asignar la fecha actual automáticamente (extrayendo solo la fecha de DateTime.Now)
                prestamo.FechaPrestamo = DateOnly.FromDateTime(DateTime.Now);

                // 2. Buscar el libro que se está prestando
                var libroPrestado = await _context.Libros.FindAsync(prestamo.IdLibro);

                if (libroPrestado != null)
                {
                    // 3. Cambiar el estado del libro a No Disponible (false)
                    libroPrestado.Estado = false;
                    _context.Update(libroPrestado);
                }

                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay error, recargar listas filtradas
            ViewData["IdLibro"] = new SelectList(_context.Libros.Where(l => l.Estado == true), "IdLibro", "Titulo", prestamo.IdLibro);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(u => u.Estado == true), "IdUsuario", "Nombre", prestamo.IdUsuario);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados.Where(e => e.Estado == true), "IdEmpleado", "Nombre", prestamo.IdEmpleado);

            return View(prestamo);
        }
    }
}