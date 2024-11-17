using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicForm.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }


        public string CommercialName { get; set; }
        public string CommonName { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}