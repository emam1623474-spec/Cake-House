using Microsoft.AspNetCore.Mvc;
using Cake_House.Models;

namespace Cake_House.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDB _context;
        public HomeController(MyDB context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cakes = _context.Cakes.Take(3).ToList();
            return View(cakes);
        }
        public IActionResult Privacy() { return View(); }
    }
}