using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class InventoryContext : IdentityDbContext<AppUser>
{
    public DbSet<Product> Products { get; set; }
    public InventoryContext(DbContextOptions options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}