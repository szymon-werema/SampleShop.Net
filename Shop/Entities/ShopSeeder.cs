using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class ShopSeeder
    {
        private readonly ModelBuilder modelBuilder;
        public ShopSeeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void SeedDatabase()
        {
            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasData(GetRoles());
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasData(GetUser());
        }
        private List<UserRole> GetRoles()
        {
            var roles = new List<UserRole>()
            {
                new UserRole()
                {
                    Id = 1,
                    Name = "User"
                },
                new UserRole()
                {
                    Id = 2,
                    Name = "Admin"
                },
                new UserRole(){
                    Id = 3,
                    Name = "Seller"
                }
            };
            return roles;
        }

        private User GetUser()
        {

            return new User()
            {
                Id = 1,
                FristName = "Admin",
                LastName = "Admin",
                Email = "admin@local.pl",
                UserRoleId = 1,
                Password = "admin"
            };
        }
    }
    
    
}
