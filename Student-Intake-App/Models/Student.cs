using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//NOTE: testing out the note feature in Visual Studio
//TODO: Setup a test project to test the StudentValidator class
namespace Student_Intake_App.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public bool IsAdmin { get; set; } = false;

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        [Display(Name = "Personal Email")]
        public string Email { get; set; } = string.Empty;

        [NotMapped] // <-- Do NOT save this to the DB
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty; // This *will* be saved in the DB


        [Required]
        [StringLength(100)]
        public string Address1 { get; set; } = string.Empty;

        // Not required, due to some addresses not having a second line
        [StringLength(100)]
        public string? Address2 { get; set; } = string.Empty;

        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        // Changed for internationalization
        [StringLength(100)]
        [Display(Name = "State/Province/Region")]
        public string Region { get; set; } = string.Empty;

        // Changed for internationalization
        [StringLength(15)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; } = string.Empty;

        // Changed for internationalization
        [Required]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [CustomValidation(typeof(StudentValidator), nameof(StudentValidator.ValidateDOB))]
        public DateTime DOB { get; set; }

        [NotMapped]
        public int Age // Automatically calculates from selected DOB
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DOB.Year;
                if (DOB > today.AddYears(-age)) age--;
                return age;
            }
        }

    }
}
