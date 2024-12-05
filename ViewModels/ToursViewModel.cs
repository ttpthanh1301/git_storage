using System.ComponentModel.DataAnnotations;

namespace TourWebsite.ViewModels;
public class ToursRequest
{
    public int Id { get; set; }
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string? TourName { get; set; }
    public IFormFile? Image { get; set; }
    public string? PlaceTour { get; set; }

    public string? Period { get; set; }
    public string? Price { get; set; }
    public string? DescriptionTour { get; set; }
    public string? ScheduledTour { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateCreated { get; set; }
    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
}
public class ToursViewModel
{
    public int Id { get; set; }
    public string? TourName { get; set; }
    public string? ImagePath { get; set; }
    public IFormFile? Image { get; set; }
    public string? PlaceTour { get; set; }
    public string? Period { get; set; }
    public string? Price { get; set; }
    public string? DescriptionTour { get; set; }
    public string? ScheduledTour { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateCreated { get; set; }
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    [Display(Name = "Category")]
    public int CategoryName { get; set; }
}