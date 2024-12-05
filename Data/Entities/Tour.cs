using System.ComponentModel.DataAnnotations;

namespace TourWebsite.Data.Entities;

public class Tours
{
    public int Id { get; set; }
    public string? TourName { get; set; }
    public string? ImagePath { get; set; }
    public string? PlaceTour { get; set; }
    public string? Period { get; set; }
    public string? Price { get; set; }
    public string? DescriptionTour { get; set; }
    public string? ScheduledTour { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateCreated { get; set; }
    public int CategoryId { get; set; }
    public Category? Categories { get; set; }
}