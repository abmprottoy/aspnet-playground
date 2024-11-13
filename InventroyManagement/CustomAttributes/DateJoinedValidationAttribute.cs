using System;
using System.ComponentModel.DataAnnotations;

namespace InventroyManagement.CustomAttributes
{
    public class DateJoinedValidationAttribute : ValidationAttribute
    {
        public DateJoinedValidationAttribute()
            : base("The Date Joined cannot be a future date.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // If the date is null, consider it invalid, since it is required
                return new ValidationResult("Date Joined is required.");
            }

            // Ensure the value is a valid DateTime
            if (value is DateTime date)
            {
                // Check if the date is in the future
                if (date > DateTime.Now)
                {
                    return new ValidationResult("Date Joined cannot be a future date.");
                }
            }
            else
            {
                // If the value is not a valid DateTime, return a validation error
                return new ValidationResult("Invalid date format.");
            }

            // If everything is valid, return Success
            return ValidationResult.Success;
        }
    }
}
