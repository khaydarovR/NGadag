using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGadag.Models;

    public class ngadag : DbContext
    {
        public ngadag (DbContextOptions<ngadag> options)
            : base(options)
        {
        }

        public DbSet<NGadag.Models.Applications> Applications { get; set; } = default!;
    }
