using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using InventroyManagement;
using InventroyManagement.Models;

namespace InventroyManagement.DTOs
{
    public class ProductDTO
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description must be within 500 characthers")]
        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be greater than or equal 0")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50)]
        public string Category { get; set; }

        // Navigation property for purchases
        public ICollection<PurchaseDTO> Purchases { get; set; }
    }



}