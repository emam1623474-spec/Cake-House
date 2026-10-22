using Cake_House.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            var cakes = await _context.Cakes.ToListAsync();
            return View(cakes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}