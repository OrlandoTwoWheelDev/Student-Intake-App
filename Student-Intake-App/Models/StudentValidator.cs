using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Intake_App.Models
{
    public class StudentValidator
    {
        public static ValidationResult ValidateDOB(DateTime dob, ValidationContext _)
        {
            if (dob > DateTime.Today)
            {
                return new ValidationResult("Date of Birth cannot be in the future.");
            }

            if (dob < DateTime.Today.AddYears(-120))
            {
                return new ValidationResult("Date of Birth cannot be more than 120 years in the past.");
            }

            return ValidationResult.Success;
        }
    }
}
