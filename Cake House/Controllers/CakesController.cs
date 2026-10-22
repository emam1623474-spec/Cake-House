
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cake_House.Models;

public class CakesController : Controller
{
    private readonly MyDB _context;
    private readonly IWebHostEnvironment _env;

    public CakesController(MyDB context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: CAKES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Cakes.ToListAsync());
    }

    // GET: CAKES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cake = await _context.Cakes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cake == null)
        {
            return NotFound();
        }

        return View(cake);
    }

    // GET: CAKES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CAKES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cake cake, IFormFile ImageFile)
    {
        if (ImageFile != null && ImageFile.Length > 0)
        {
            string wwwRootPath = _env.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            string path = Path.Combine(wwwRootPath, "images", fileName);

            Directory.CreateDirectory(Path.Combine(wwwRootPath, "images"));

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            cake.ImagePath = "/images/" + fileName;
        }

        _context.Add(cake);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: CAKES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cake = await _context.Cakes.FindAsync(id);
        if (cake == null)
        {
            return NotFound();
        }
        return View(cake);
    }

    // POST: CAKES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Description,Price,Pound,ImagePath")] Cake cake)
    {
        if (id != cake.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cake);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CakeExists(cake.Id))
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
        return View(cake);
    }

    // GET: CAKES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cake = await _context.Cakes
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cake == null)
        {
            return NotFound();
        }

        return View(cake);
    }

    // POST: CAKES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var cake = await _context.Cakes.FindAsync(id);
        if (cake != null)
        {
            _context.Cakes.Remove(cake);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CakeExists(int? id)
    {
        return _context.Cakes.Any(e => e.Id == id);
    }
}
