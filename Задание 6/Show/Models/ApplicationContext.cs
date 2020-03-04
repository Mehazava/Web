using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Show.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            if (!(Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                throw new Exception("No database found.");
            }
        }
    }
}
