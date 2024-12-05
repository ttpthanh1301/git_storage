using Microsoft.EntityFrameworkCore;
using TourWebsite.Data;
using TourWebsite.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Google.Api;
using System;

namespace Tour.Data
{
    public class DbInitializer
    {
        private readonly TourDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string AdminRoleName = "Admin";
        private readonly string UserRoleName = "Member";
        public DbInitializer(TourDbContext context,
          UserManager<User> userManager,
          RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Seed()
        {
            //seeding role
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = AdminRoleName,
                    NormalizedName = AdminRoleName.ToUpper(),
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = UserRoleName,
                    NormalizedName = UserRoleName.ToUpper(),
                });
            }
            // Seeding user
            if (!_userManager.Users.Any())
            {
                var result = await _userManager.CreateAsync(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "example@gmail.com",
                    FullName = "Example",
                    Email = "example@gmail.com",
                    LockoutEnabled = false,
                    PhoneNumber = "0987654321",
                    Dob = new DateTime(2000, 01, 01)
                }, "Admin@123");
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync("example@gmail.com");
                    if (user != null)
                        await _userManager.AddToRoleAsync(user, AdminRoleName);
                }
            }
            var category_2 = new Category
            {
                CategoryName = "Ninh Bình",
                ReleaseDate = DateTime.Today,
            };
            var category_3 = new Category
            {
                CategoryName = "Hà Nội",
                ReleaseDate = DateTime.Today,
            };
            var category_4 = new Category
            {
                CategoryName = "Ho Chi Minh",
                ReleaseDate = DateTime.Today,
            };
            var category_5 = new Category
            {
                CategoryName = "Hà Giang",
                ReleaseDate = DateTime.Today,
            };
            var category_6 = new Category
            {
                CategoryName = "Da Nang",
                ReleaseDate = DateTime.Today,
            };
            var tour_1 = new Tours
            {
                TourName = "Hà Nội Adventure",
                PlaceTour = "Hà Nội",
                Period = "3 days",
                Price = "1500000",  // Price as string
                DescriptionTour = "Explore the scenic beauty of Hà Nội with a guided tour.",
                ScheduledTour = "10/12/2024",
                DateCreated = DateTime.Today,
                CategoryId = _context.Categories.First(c => c.CategoryName == "Hà Nội").Id,
            };
            var tour_2 = new Tours
            {
                TourName = "Ho Chi Minh Adventure",
                PlaceTour = "Ho Chi Minh",
                Period = "3 days",
                Price = "1500000",  // Price as string
                DescriptionTour = "Explore the scenic beauty of Ho Chi Minh with a guided tour.",
                ScheduledTour = "10/12/2024",
                DateCreated = DateTime.Today,
                CategoryId = _context.Categories.First(c => c.CategoryName == "Ho Chi Minh").Id,
            };
            var tour_3 = new Tours
            {
                TourName = "Hà Giang Adventure",
                PlaceTour = "Hà Giang",
                Period = "3 days",
                Price = "1500000",  // Price as string
                DescriptionTour = "Explore the scenic beauty of Hà Giang with a guided tour.",
                ScheduledTour = "10/12/2024",
                DateCreated = DateTime.Today,
                CategoryId = _context.Categories.First(c => c.CategoryName == "Hà Giang").Id,
            };
            var tour_4 = new Tours
            {
                TourName = "Da Nang Adventure",
                PlaceTour = "Da Nang",
                Period = "3 days",
                Price = "1500000",  // Price as string
                DescriptionTour = "Explore the scenic beauty of Da Nang with a guided tour.",
                ScheduledTour = "10/12/2024",
                DateCreated = DateTime.Today,
                CategoryId = _context.Categories.First(c => c.CategoryName == "Da Nang").Id,
            };
            var tour_5 = new Tours
            {
                TourName = "Ninh Bình Adventure",
                PlaceTour = "Ninh Bình",
                Period = "3 days",
                Price = "1500000",  // Price as string
                DescriptionTour = "Explore the scenic beauty of Ninh Bình with a guided tour.",
                ScheduledTour = "10/12/2024",
                DateCreated = DateTime.Today,
                CategoryId = _context.Categories.First(c => c.CategoryName == "Ninh Bình").Id,
            };
            if (!_context.Tours.Any())
            {
                _context.Tours.AddRange(tour_1, tour_2, tour_3, tour_4, tour_5);
                await _context.SaveChangesAsync();
            }
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(category_6, category_2, category_3, category_4, category_5);
                await _context.SaveChangesAsync();
            }

        }
    }
}
