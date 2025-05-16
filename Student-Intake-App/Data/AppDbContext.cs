using Microsoft.EntityFrameworkCore;
using Student_Intake_App.Models;

namespace Student_Intake_App.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; } = null!;
    }
    
}