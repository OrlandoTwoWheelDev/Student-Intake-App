using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Intake_App.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [NotMapped] // <-- Do NOT save this to the DB
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // This *will* be saved in the DB


        [Required]
        [StringLength(100)]
        public string Address1 { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Address2 { get; set; } = string.Empty;

        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; } = string.Empty;

        [StringLength(10)]
        public string ZipCode { get; set; } = string.Empty;

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [CustomValidation(typeof(StudentValidator), nameof(StudentValidator.ValidateDOB))]
        public DateTime DOB { get; set; }

        [Range(1, 120)]
        public int? Age { get; set; }
    }
}
