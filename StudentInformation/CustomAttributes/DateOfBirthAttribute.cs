using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentInformation.CustomAttributes
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public int MinimumAge { get; set; }

        public DateOfBirthAttribute(int minimumAge)
        {
            MinimumAge = minimumAge;

            ErrorMessage = ErrorMessage ?? $"Date of birth must be at least {MinimumAge} years ago.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Date of Birth is required.");
            }

            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;


                if (age >= MinimumAge)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}