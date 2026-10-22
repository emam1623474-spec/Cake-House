using Microsoft.EntityFrameworkCore;

namespace Cake_House.Models
{
    public class MyDB : DbContext
    {
        public MyDB(DbContextOptions<MyDB> options) : base(options) { }

        public DbSet<Cake> Cakes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cake>().HasData(
                new Cake { Id = 1, Name = "Chocolate Delight", Price = 650, ImagePath = "https://images.unsplash.com/photo-1578985545062-69928b1d9587?w=500" },
                new Cake { Id = 2, Name = "Vanilla Dream", Price = 550, ImagePath = "https://images.unsplash.com/photo-1588195538326-c407024dee92?w=500" },
                new Cake { Id = 3, Name = "Red Velvet", Price = 750, ImagePath = "https://images.unsplash.com/photo-1614707267537-b85aaf00c4b7?w=500" },
                new Cake { Id = 4, Name = "Strawberry Cake", Price = 600, ImagePath = "https://images.unsplash.com/photo-1464349095431-e9a21285b5f3?w=500" }
            );
        }
    }
}