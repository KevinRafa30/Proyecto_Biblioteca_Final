using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BibliotecaUNAPEC_Web.Models; 
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize]
    public class LibrosController : Controller
    {

        private readonly BibliotecaUnapecContext _context;

        //inyeccion de la base de datos
        public LibrosController(BibliotecaUnapecContext context)
        {
            _context = context;
        }

        //formulario para nuevo libro
        //get /Libros/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}
