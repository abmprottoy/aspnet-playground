using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreIntroduction.Models;

namespace CoreIntroduction.Data
{
    public class CoreIntroductionContext : DbContext
    {
        public CoreIntroductionContext (DbContextOptions<CoreIntroductionContext> options)
            : base(options)
        {
        }

        public DbSet<CoreIntroduction.Models.Movie> Movie { get; set; } = default!;
    }
}
