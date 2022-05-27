using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class LocalDbContext : DbContext
    {
        private readonly IPasswordHasher<User> passwordHasher;

        public LocalDbContext(DbContextOptions<LocalDbContext> options, IPasswordHasher<User> passwordHasher) : base(options)
        {
            this.passwordHasher = passwordHasher;
        }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ShopSeeder(modelBuilder, passwordHasher).SeedDatabase();

            modelBuilder.Entity<User>(eb =>
           {
               
           });


            modelBuilder.Entity<UserRole>(eb =>
            {
                
            });
        }
    }
}
