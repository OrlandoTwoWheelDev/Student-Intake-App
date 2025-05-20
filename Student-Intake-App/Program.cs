using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Intake_App.Data;
using Student_Intake_App.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new RequireHttpsAttribute()); //Global HTTPS enforcement
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAntiforgery(options =>
{ //layered protection
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {   // Cookie settings for authentication routes
        options.LoginPath = "/Login/Index";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Setting up the Admin user in the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Only add if one doesn't already exist
    if (!context.Students.Any(s => s.Email == "admin@example.com"))
    {
        var passwordHasher = new PasswordHasher<Student>();
        var admin = new Student
        {
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@example.com",
            PasswordHash = passwordHasher.HashPassword(null, "Admin123!"),
            IsAdmin = true,
            Address1 = "123 Admin St",
            City = "Adminville",
            Region = "FL",
            PostalCode = "12345",
            PhoneNumber = "555-555-5555",
            DOB = new DateTime(1990, 1, 1),
        };

        context.Students.Add(admin);
        context.SaveChanges();
    }
}

// Force HTTPS and HSTS even in non-dev environments
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
