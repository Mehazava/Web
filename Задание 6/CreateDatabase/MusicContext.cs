using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateDatabase
{
    class MusicContext : DbContext
    {
        public MusicContext(string connection) : base(connection) 
        {
            if (Database.Exists())
            {
                Database.Delete();
                Database.CreateIfNotExists();
            }
        }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Producer> Producers { get; set; }
    }
}
