using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddDbContext<Cake_House.Models.MyDB>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
app.UseSession();
// Safe Migrate - Fail করলেও অ্যাপ Crash করবে না
try
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<Cake_House.Models.MyDB>();
        db.Database.Migrate();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Migrate failed: {ex.Message}");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Cake_House.Models.MyDB>();
    try
    {
        if (!db.Cakes.Any())
        {
            db.Cakes.AddRange(
                new Cake_House.Models.Cake { Name = "Chocolate Delight", Price = 650, ImagePath = "https://images.unsplash.com/photo-1578985545062-69928b1d9587?w=500" },
                new Cake_House.Models.Cake { Name = "Vanilla Dream", Price = 550, ImagePath = "https://images.unsplash.com/photo-1588195538326-c407024dee92?w=500" },
                new Cake_House.Models.Cake { Name = "Red Velvet", Price = 750, ImagePath = "https://images.unsplash.com/photo-1614707267537-b85aaf00c4b7?w=500" },
                new Cake_House.Models.Cake { Name = "Strawberry Cake", Price = 600, ImagePath = "https://images.unsplash.com/photo-1464349095431-e9a21285b5f3?w=500" }
            );
            db.SaveChanges();
        }
    }
    catch { }
}

app.Run();