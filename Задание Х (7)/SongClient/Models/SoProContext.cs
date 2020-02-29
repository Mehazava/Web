using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongClient.Models
{
    public class SoProContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public SoProContext(DbContextOptions<SoProContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
