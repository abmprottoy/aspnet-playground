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
        [RegularExpression(@"^[a-zA-Z .-]+$", ErrorMessage = "Name may only contain Small letters (a-z), Capital letters (A-Z), Dot (.), Hyphen (-)")]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}-\d{5}-[1-3]$", ErrorMessage ="ID must be in AIUB ID format. (XX-XXXXX-X)")]
        public string ID { get; set; }

        [Required]
        [EmailAddress]
        [EmailMatchesID(ErrorMessage = "Email must be in the AIUB student mail format (<ID>@student.aiub.edu) and match the given ID.")]
        public string Email { get; set; }

        [Required]
        [DateOfBirth(20, ErrorMessage = "Age must be at least 20 years old")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-YYYY}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
    }
}