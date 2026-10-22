using Microsoft.EntityFrameworkCore;

namespace Cake_House.Models
{
    public class MyDB : DbContext
    {
        public MyDB(DbContextOptions<MyDB> options) : base(options) { }
        public DbSet<Cake> Cakes { get; set; }
    }
}