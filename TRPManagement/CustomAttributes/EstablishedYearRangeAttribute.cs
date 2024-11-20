using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TRPManagement.EF;

namespace TRPManagement.CustomAttributes
{
    public class EstablishedYearRangeAttribute : ValidationAttribute
    {
        private int _minYear = 1900;
        private int _maxYear = DateTime.Now.Year;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Established Year is required.");
            }

            int year = (int)value;
            if (year < _minYear || year > _maxYear)
            {
                return new ValidationResult($"Established Year must be between {_minYear} and {_maxYear}.");
            }

            return ValidationResult.Success;
        }
    }

}