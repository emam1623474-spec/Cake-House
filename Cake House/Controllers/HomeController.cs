using Microsoft.AspNetCore.Mvc;
using Cake_House.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cake_House.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDB _context;
        public HomeController(MyDB context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cakes = await _context.Cakes.Take(4).ToListAsync();
            return View(cakes);
        }

        public IActionResult Privacy() => View();
    }
}