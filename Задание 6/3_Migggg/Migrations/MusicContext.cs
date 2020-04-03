using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrate
{
    class MusicContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public MusicContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MigrDb;Trusted_Connection=True;");
        }
    }
}
