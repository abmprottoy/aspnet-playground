using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TRPManagement.EF;

namespace TRPManagement.CustomAttributes
{
    public class UniqueChannelNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Add logic to check if the ChannelName is unique in the database
            var dbContext = new TRPManagementEntities();
            var channelName = value as string;

            if (dbContext.Channels.Any(c => c.ChannelName == channelName))
            {
                return new ValidationResult("Channel name must be unique.");
            }

            return ValidationResult.Success;
        }
    }
}