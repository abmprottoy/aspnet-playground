using System;
using System.ComponentModel.DataAnnotations;

namespace TRPManagement.DTOs
{
    public class ProgramDTO
    {
        public int ProgramId { get; set; }

        [StringLength(150)]
        [Required]
        public string ProgramName { get; set; }

        [Range(0.00, 10.00, ErrorMessage = "Rating must be between 0.00 and 10.00")]
        [Required]
        public decimal TRPScore { get; set; }

        [Required]
        public int ChannelId { get; set; }

        [Required]
        public DateTime AirTime { get; set; }
    }
}
