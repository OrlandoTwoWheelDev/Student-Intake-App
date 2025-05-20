using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Intake_App.Models
{
    public class StudentValidator
    {
        public static ValidationResult? ValidateDOB(DateTime dob, ValidationContext _)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--; // Adjust if their birthday hasn't occurred yet this year

            return (age >= 15 && age <= 85)
                ? ValidationResult.Success
                : new ValidationResult("Age must be between 15 and 85.");
        }
    }
}
