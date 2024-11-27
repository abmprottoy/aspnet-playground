using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.DTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        [StringLength(13)]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Invalid ISBN number.")]
        public string ISBN { get; set; }

        [Required]
        [Range(1000, int.MaxValue, ErrorMessage = "Invalid publication year.")]
        public int PublicationYear { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Copies available cannot be negative.")]
        public int CopiesAvailable { get; set; }
    }

}