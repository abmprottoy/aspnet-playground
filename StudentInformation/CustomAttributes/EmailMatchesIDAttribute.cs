using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StudentInformation.CustomAttributes
{
    public class EmailMatchesIDAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check if email is provided
            if (value == null || !(value is string email))
            {
                return new ValidationResult("Email is required.");
            }

            // Get the ID property value from the validation context
            var idProperty = validationContext.ObjectType.GetProperty("ID");
            if (idProperty == null)
            {
                return new ValidationResult("ID property not found.");
            }

            var idValue = idProperty.GetValue(validationContext.ObjectInstance) as string;
            if (string.IsNullOrEmpty(idValue))
            {
                return new ValidationResult("ID is required.");
            }

            // Check if email matches the required format <ID>@student.aiub.edu
            var expectedEmail = $"{idValue}@student.aiub.edu";
            if (!email.Equals(expectedEmail, StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult($"Email must match the format {idValue}@student.aiub.edu.");
            }

            return ValidationResult.Success;
        }
    }
}