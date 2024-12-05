using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tour.Data;
using TourWebsite.Data;
using TourWebsite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//
builder.Services.AddDbContext<TourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TourDbContext") ?? throw new InvalidOperationException("Connection string 'TourDbContext' not found.")));
//automapper config
builder.Services.AddAutoMapper(typeof(Program));
//interface
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IToursService, ToursService>();
builder.Services.AddTransient<IStorageService, FileStorageService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    DbInitializer.Seed(services);
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
