using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tour.Data;
using TourWebsite.Data;
using TourWebsite.Data.Entities;
using TourWebsite.Services;
using TourWebsite.ViewModels;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TourDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TourDbContext") ?? throw new InvalidOperationException("Connection string 'TourDbContext' not found.")));

// Automapper config
builder.Services.AddAutoMapper(typeof(Program));

// Interface services
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IToursService, ToursService>();
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<DbInitializer>();
//Configuring the Authentication Service
// Swagger config
// Setup identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<TourDbContext>()
    .AddDefaultTokenProviders();

// JWT Authentication config (Chuyển lên trước `builder.Build()`)
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
// Swagger config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger Course Management", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
var jwtOptions = builder.Configuration
    .GetSection("JwtOptions")
    .Get<JwtOptions>();

builder.Services.AddSingleton(jwtOptions!);

//Configuring the Authentication Service
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts =>
{
    //convert the string signing key to byte array

    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions!.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = System.TimeSpan.Zero,
        
        IssuerSigningKey = new SymmetricSecurityKey((byte[]?)Encoding.UTF8
        .GetBytes(jwtOptions!.SigningKey!))
    };
});
var app = builder.Build(); // Đảm bảo Build() được gọi sau tất cả các cấu hình

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Course Management V1");
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TourDbContext>();
    db.Database.Migrate();

    try
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Seeding data...");
        var dbInitializer = scope.ServiceProvider.GetService<DbInitializer>();
        dbInitializer?.Seed().Wait();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

