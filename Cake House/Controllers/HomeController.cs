using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cake_House.Models;
using Microsoft.EntityFrameworkCore;
using Cake_House.Data;

namespace Cake_House.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cakes = await _context.Cakes.Take(4).ToListAsync();
            return View(cakes);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}