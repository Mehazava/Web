using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class CarContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string moderatorRoleName = "moderator";
            string consultantRoleName = "consultant";

            string adminEmail = "a";
            string adminPassword = "1";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role moderatorRole = new Role { Id = 2, Name = moderatorRoleName };
            Role consultantRole = new Role { Id = 3, Name = consultantRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, moderatorRole, consultantRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
