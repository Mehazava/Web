using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Show.Models
{
    public class LazyContext : DbContext
    {
        public DbSet<LazySong> Songs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public LazyContext(DbContextOptions<LazyContext> options)
            : base(options)
        {
            if (!(Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                throw new Exception("No database found.");
            }
        }
    }
}
