using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventroyManagement.DTOs
{
    public class PurchaseDTO
    {
        public int PurchaseId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public DateTime? PurchaseDate { get; set; } = DateTime.Now; // Default to the current date if not provided

        // Navigation properties
        public CustomerDTO Customer { get; set; }
        public ProductDTO Product { get; set; }
    }
}