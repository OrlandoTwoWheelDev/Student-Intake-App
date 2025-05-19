using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Student_Intake_App.Data;
using Student_Intake_App.Models;


namespace Student_Intake_App.Controllers
{
    [RequireHttps] // Move the attribute inside the namespace and apply it to the class
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == email);

            if (student == null || string.IsNullOrEmpty(student.Email) || !BCrypt.Net.BCrypt.Verify(password, student.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            }

            var claims = new List<Claim>
                                {
                                    new(ClaimTypes.Name, student.Email ?? string.Empty), // Ensure non-null value
                                    new("StudentId", student.StudentId.ToString()),
                                    new(ClaimTypes.Role, student.IsAdmin ? "Admin" : "User")
                                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return student.IsAdmin ? RedirectToAction("Index", "Admin") : RedirectToAction("MyAccount", "Students");
        }
    }

}
