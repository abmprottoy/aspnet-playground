using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TRPManagement.CustomAttributes;

namespace TRPManagement.DTOs
{
    public class ChannelDTO
    {
        public int ChannelId { get; set; }

        [UniqueChannelName]
        [StringLength(100)]
        [Required]
        public string ChannelName { get; set; }

        [EstablishedYearRange]
        [Required]
        public int EstablishedYear { get; set; }

        [StringLength(50)]
        [Required]
        public string Country { get; set; }

        public virtual ICollection<ProgramDTO> Programs { get; set; } = new List<ProgramDTO>();
    }
}
