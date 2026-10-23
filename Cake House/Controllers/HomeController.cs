using Microsoft.AspNetCore.Mvc;
using Cake_House.Models;

namespace Cake_House.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDB _context;
        public HomeController(MyDB context) { _context = context; }

        public IActionResult Index()
        {
            var cakes = _context.Cakes.ToList();

            // DB ফাঁকা থাকলেও ৩টা কেক দেখাবে যাতে Background ফাঁকা না থাকে
            if (cakes.Count == 0)
            {
                var cake = new List<Cake>
            {
                 new Cake { Id = 1, Name = "Chocolate Delight", Price = 650, ImagePath = "https://images.unsplash.com/photo-1578985545062-69928b1d9587?w=600" },
                 new Cake { Id = 2, Name = "Vanilla Dream", Price = 550, ImagePath = "https://images.unsplash.com/photo-1565958011703-44f9829ba187?w=600" },
                 new Cake { Id = 3, Name = "Red Velvet", Price = 700, ImagePath = "https://images.unsplash.com/photo-1614707267537-b85aaf00c4b7?w=600" }
            };
            }
            return View(cakes.Take(3).ToList());
        }

        public IActionResult Privacy() => View();
    }
}