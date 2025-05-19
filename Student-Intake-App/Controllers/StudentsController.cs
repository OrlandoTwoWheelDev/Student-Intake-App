using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Intake_App.Data;
using Student_Intake_App.Models;

namespace Student_Intake_App.Controllers
{
    [RequireHttps]
    public class StudentsController(AppDbContext context) : Controller
    {

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,Email,Password,Address1,Address2,City,State,ZipCode,PhoneNumber,DOB,Age,IsAdmin")] Student student)
        {
            if (ModelState.IsValid)
            {
                var hasher = new PasswordHasher<Student>();
                student.PasswordHash = hasher.HashPassword(student, student.PasswordHash);

                context.Add(student);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState)
                {
                    Console.WriteLine($"{modelError.Key}: {string.Join(", ", modelError.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,Email,Password,Address1,Address2,City,State,ZipCode,PhoneNumber,DOB,Age")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(student);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student != null)
            {
                context.Students.Remove(student);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return context.Students.Any(e => e.StudentId == id);
        }

        [Authorize]
        public async Task<IActionResult> MyAccount()
        {
            var studentId = User.Claims.FirstOrDefault(c => c.Type == "StudentId")?.Value;
            if (studentId == null)
                return RedirectToAction("Login", "Login");

            var student = await context.Students.FindAsync(int.Parse(studentId));
            return View("Details", student);
        }
    }
}
