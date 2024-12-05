using System.ComponentModel.DataAnnotations;

namespace TourWebsite.ViewModels;

public class CategoriesRequest
{
    public int Id { get; set; }
    [StringLength(60)]
    [Required]
    public string? CategoryName { get; set; }
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
}

public class CategoriesViewModel
{

    public int Id { get; set; }
    public string? CategoryName { get; set; }
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
}