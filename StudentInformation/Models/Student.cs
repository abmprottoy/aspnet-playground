using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using StudentInformation.CustomAttributes;

namespace StudentInformation.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Please provide name")]
        [RegularExpression(@"^[a-zA-Z .-]+$", ErrorMessage = "Name should only contains Small letters (a-z), Capital letters (A-Z) and Dot (.) or Hyphen (-)")]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}-\d{5}-[1-3]$", ErrorMessage ="ID must be in AIUB ID Format. (XX-XXXXX-X)")]
        public string ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DateOfBirth(20, ErrorMessage = "Age must be at least 20 years old")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
    }
}