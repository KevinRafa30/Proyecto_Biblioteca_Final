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
    public class CienciasController : Controller
    {
        private readonly BibliotecaUnapecContext _context;

        public CienciasController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        // GET: Ciencias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ciencias.ToListAsync());
        }

        // GET: Ciencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciencia = await _context.Ciencias
                .FirstOrDefaultAsync(m => m.IdCiencia == id);
            if (ciencia == null)
            {
                return NotFound();
            }

            return View(ciencia);
        }

        // GET: Ciencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ciencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCiencia,Descripcion,Estado")] Ciencia ciencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ciencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ciencia);
        }

        // GET: Ciencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciencia = await _context.Ciencias.FindAsync(id);
            if (ciencia == null)
            {
                return NotFound();
            }
            return View(ciencia);
        }

        // POST: Ciencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCiencia,Descripcion,Estado")] Ciencia ciencia)
        {
            if (id != ciencia.IdCiencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ciencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CienciaExists(ciencia.IdCiencia))
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
            return View(ciencia);
        }

        // GET: Ciencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciencia = await _context.Ciencias
                .FirstOrDefaultAsync(m => m.IdCiencia == id);
            if (ciencia == null)
            {
                return NotFound();
            }

            return View(ciencia);
        }

        // POST: Ciencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciencia = await _context.Ciencias.FindAsync(id);
            if (ciencia != null)
            {
                _context.Ciencias.Remove(ciencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CienciaExists(int id)
        {
            return _context.Ciencias.Any(e => e.IdCiencia == id);
        }
    }
}
