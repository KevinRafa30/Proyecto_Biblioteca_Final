using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BibliotecaUNAPEC_Web.Models; 
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUNAPEC_Web.Controllers
{
    [Authorize]
    public class LibrosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
