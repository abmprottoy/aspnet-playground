using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using TRPManagement.DTOs;
using TRPManagement.EF;

namespace TRPManagement.CustomAttributes
{
    public class UniqueProgramNameWithinChannelAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Validate only if the required properties exist
            var programDTO = validationContext.ObjectInstance as ProgramDTO;

            if (programDTO == null)
            {
                return new ValidationResult("Validation context is not a ProgramDTO.");
            }

            // Access the associated channel's programs from the validation context
            var channelProperty = validationContext.ObjectType.GetProperty("ChannelId");

            if (channelProperty == null)
            {
                return new ValidationResult("ChannelId property not found.");
            }

            var dbContext = (TRPManagementEntities)validationContext.GetService(typeof(TRPManagementEntities));
            if (dbContext == null)
            {
                return new ValidationResult("Unable to resolve the DbContext.");
            }

            // Check if a program with the same name exists in the channel
            var isDuplicate = dbContext.Programs
                .Any(p => p.ProgramName == programDTO.ProgramName && p.ChannelId == programDTO.ChannelId);

            return isDuplicate
                ? new ValidationResult("The program name must be unique within the channel.")
                : ValidationResult.Success;
        }
    }
}
