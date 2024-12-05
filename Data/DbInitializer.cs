using Microsoft.EntityFrameworkCore;
using TourWebsite.Data;
using TourWebsite.Data.Entities;

namespace Tour.Data
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new TourDbContext(serviceProvider.GetRequiredService<DbContextOptions<TourDbContext>>()))
            {
                // Look for any categories or tours.
                if (context.Categories.Any() && context.Tours.Any())
                {
                    return;   // DB has been seeded
                }

                // Add Categories
                context.Categories.AddRange(
                    new Category
                    {
                        CategoryName = "Ninh Bình",
                        ReleaseDate = DateTime.Today,
                    },
                    new Category
                    {
                        CategoryName = "Hà Nội",
                        ReleaseDate = DateTime.Today,
                    },
                    new Category
                    {
                        CategoryName = "Ho Chi Minh",
                        ReleaseDate = DateTime.Today,
                    },
                    new Category
                    {
                        CategoryName = "Hà Giang",
                        ReleaseDate = DateTime.Today,
                    },
                    new Category
                    {
                        CategoryName = "Da Nang",
                        ReleaseDate = DateTime.Today,
                    }
                );

                // Save categories before adding tours (if CategoryId is a foreign key)
                context.SaveChanges();

                // Add Tours
                context.Tours.AddRange(
                    new Tours
                    {
                        TourName = "Ninh Bình Adventure",
                        PlaceTour = "Ninh Bình",
                        Period = "3 days",
                        Price = "1500000",  // Price as string
                        DescriptionTour = "Explore the scenic beauty of Ninh Bình with a guided tour.",
                        ScheduledTour = "10/12/2024",
                        DateCreated = DateTime.Today,
                        CategoryId = context.Categories.First(c => c.CategoryName == "Ninh Bình").Id,
                    },
                    new Tours
                    {
                        TourName = "Hà Nội City Tour",
                        PlaceTour = "Hà Nội",
                        Period = "2 days",
                        Price = "1200000",
                        DescriptionTour = "Discover the rich culture and history of Hà Nội.",
                        ScheduledTour = "05/12/2024",
                        DateCreated = DateTime.Today,
                        CategoryId = context.Categories.First(c => c.CategoryName == "Hà Nội").Id,
                    },
                    new Tours
                    {
                        TourName = "Ho Chi Minh Experience",
                        PlaceTour = "Ho Chi Minh",
                        Period = "5 days",
                        Price = "1800000",
                        DescriptionTour = "A tour of the vibrant and historical Ho Chi Minh City.",
                        ScheduledTour = "15/12/2024",
                        DateCreated = DateTime.Today,
                        CategoryId = context.Categories.First(c => c.CategoryName == "Ho Chi Minh").Id,
                    },
                    new Tours
                    {
                        TourName = "Hà Giang Highland Trek",
                        PlaceTour = "Hà Giang",
                        Period = "7 days",
                        Price = "2000000",
                        DescriptionTour = "Trek through the stunning landscapes of Hà Giang.",
                        ScheduledTour = "20/12/2024",
                        DateCreated = DateTime.Today,
                        CategoryId = context.Categories.First(c => c.CategoryName == "Hà Giang").Id,
                    },
                    new Tours
                    {
                        TourName = "Da Nang Beach Escape",
                        PlaceTour = "Da Nang",
                        Period = "4 days",
                        Price = "1700000",
                        DescriptionTour = "Relax and enjoy the beachside in Da Nang.",
                        ScheduledTour = "25/12/2024",
                        DateCreated = DateTime.Today,
                        CategoryId = context.Categories.First(c => c.CategoryName == "Da Nang").Id,
                    }
                );

                // Save tours to the database
                context.SaveChanges();
            }
        }
    }
}
