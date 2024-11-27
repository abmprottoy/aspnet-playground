using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage ="User Name is required")]
        [Display(Name = "Member Name")]
        [StringLength(50, ErrorMessage = "User Name must be less than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Member Email is required")]
        [Display(Name = "Member Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
    }
}