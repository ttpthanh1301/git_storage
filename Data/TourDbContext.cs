using Microsoft.EntityFrameworkCore;
using TourWebsite.Data.Entities;

namespace TourWebsite.Data;

public class TourDbContext : DbContext
{
    public TourDbContext(DbContextOptions<TourDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
    }

    public DbSet<Tours> Tours { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
}