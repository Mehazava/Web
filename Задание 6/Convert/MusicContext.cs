using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrate
{
    class MusicContext : DbContext
    {
        public MusicContext() : base("DBConnection")
        {
            if (!(Database.Exists()))
            {
                throw new Exception("No database to convert.");
            }
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicContext, Convert.Migrations.Configuration>());
        }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Producer> Producers { get; set; }
    }
}
