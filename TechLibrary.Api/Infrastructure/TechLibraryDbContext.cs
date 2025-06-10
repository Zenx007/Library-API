using Microsoft.EntityFrameworkCore;

namespace TechLibrary.Api.Infrastructure;

public class TechLibraryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source");
    }
}
