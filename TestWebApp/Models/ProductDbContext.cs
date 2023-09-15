using Microsoft.EntityFrameworkCore;

namespace TestWebApp.Models;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server =(localdb)\mssqllocaldb");
    }
}
