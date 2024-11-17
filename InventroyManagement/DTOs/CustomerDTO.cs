using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InventroyManagement.CustomAttributes;

namespace InventroyManagement.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Phone number must be between 10 to 15 digits.")]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [DateJoinedValidation]
        public System.DateTime DateJoined { get; set; } = DateTime.Now; //If Date Joined is not choose custom attribute will set it to current date by default

        // Navigation property for purchases
        public ICollection<PurchaseDTO> Purchases { get; set; }
    }
}