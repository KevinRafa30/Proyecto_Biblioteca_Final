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
                .Include(p => p.IdLibroNavigation)  
                .Include(p => p.IdUsuarioNavigation) 
                .Include(p => p.IdEmpleadoNavigation)
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
        public async Task<IActionResult> Create([Bind("IdTransaccion,IdEmpleado,IdLibro,IdUsuario,FechaPrestamo,FechaDevolucion,MontoPorDia,CantidadDias,Comentario,Estado")] PrestamosDevolucione prestamo)
        {
            ModelState.Remove("FechaPrestamo");

            if (ModelState.IsValid)
            {
                // 1. Asignar la fecha actual automáticamente
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

            ViewData["IdLibro"] = new SelectList(_context.Libros.Where(l => l.Estado == true), "IdLibro", "Titulo", prestamo.IdLibro);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(u => u.Estado == true), "IdUsuario", "Nombre", prestamo.IdUsuario);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados.Where(e => e.Estado == true), "IdEmpleado", "Nombre", prestamo.IdEmpleado);

            return View(prestamo);
        }

        // GET: PrestamosDevoluciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.PrestamosDevoluciones.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            // Cargar las listas sin filtrar por estado activo, 
            // porque el libro o usuario original podría estar inactivo ahora, 
            // pero necesitamos mostrar su nombre en la vista de solo lectura.
            ViewData["IdLibro"] = new SelectList(_context.Libros, "IdLibro", "Titulo", prestamo.IdLibro);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", prestamo.IdUsuario);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombre", prestamo.IdEmpleado);

            return View(prestamo);
        }

        // POST: PrestamosDevoluciones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaccion,IdEmpleado,IdLibro,IdUsuario,FechaPrestamo,FechaDevolucion,MontoPorDia,CantidadDias,Comentario,Estado")] PrestamosDevolucione prestamo)
        {
            if (id != prestamo.IdTransaccion)
            {
                return NotFound();
            }

            // Removemos la validación de la fecha de devolución porque el sistema la asignará
            ModelState.Remove("FechaDevolucion");

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Estampar la fecha real en que el libro fue entregado
                    prestamo.FechaDevolucion = DateOnly.FromDateTime(DateTime.Now);

                    // 2. Liberar el libro devolviendo su estado a Disponible (true)
                    var libroDevuelto = await _context.Libros.FindAsync(prestamo.IdLibro);
                    if (libroDevuelto != null)
                    {
                        libroDevuelto.Estado = true;
                        _context.Update(libroDevuelto);
                    }

                    // 3. Guardar los cambios (incluyendo el comentario insertado en la vista)
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.IdTransaccion))
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

            ViewData["IdLibro"] = new SelectList(_context.Libros, "IdLibro", "Titulo", prestamo.IdLibro);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", prestamo.IdUsuario);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleados, "IdEmpleado", "Nombre", prestamo.IdEmpleado);

            return View(prestamo);
        }

        // GET: PrestamosDevoluciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Usamos .Include para traer toda la información relacionada
            var prestamo = await _context.PrestamosDevoluciones
                .Include(p => p.IdEmpleadoNavigation)
                .Include(p => p.IdLibroNavigation)
                .Include(p => p.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }
        private bool PrestamoExists(int id)
        {
            return _context.PrestamosDevoluciones.Any(e => e.IdTransaccion == id);
        }
    }
}