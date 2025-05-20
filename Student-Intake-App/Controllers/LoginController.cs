using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Student_Intake_App.Data;
using Student_Intake_App.Models;


namespace Student_Intake_App.Controllers
{
    [RequireHttps]
    public class LoginController : Controller
    {
        public LoginController(AppDbContext context) => _context = context;

            private readonly AppDbContext _context;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == email);

            if (student == null || string.IsNullOrEmpty(student.Email))
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            }

            var hasher = new PasswordHasher<Student>();
            var verificationResult = hasher.VerifyHashedPassword(student, student.PasswordHash, password);
                // switched from Bcrypt to PasswordHasher for consistency with other parts of the code
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, student.Email ?? string.Empty),
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
