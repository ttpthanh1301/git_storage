using Microsoft.EntityFrameworkCore;
using TourWebsite.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TourWebsite.Data;

public class TourDbContext : IdentityDbContext<User>
{
    public TourDbContext(DbContextOptions<TourDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().ToTable("Roles").Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        modelBuilder.Entity<User>().ToTable("Users").Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
    }

    public DbSet<Tours> Tours { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
}