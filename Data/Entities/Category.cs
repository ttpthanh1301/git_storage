using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TourWebsite.Data.Entities;

public class Category
{
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    public ICollection<Tours>? Tours { get; set; }
}
public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(e => e.Tours)
        .WithOne(e => e.Categories)
        .HasForeignKey(e => e.CategoryId)
        .HasPrincipalKey(e => e.Id);
    }
}