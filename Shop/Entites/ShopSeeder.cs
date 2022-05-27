using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPasswordHasher<User> passwordHasher;

        public ShopSeeder(ModelBuilder modelBuilder,
            IPasswordHasher<User> passwordHasher)
        {
            this.modelBuilder = modelBuilder;
            this.passwordHasher = passwordHasher;
        }
        public void SeedDatabase()
        {
            modelBuilder.Entity<UserRole>(eb =>
           {
               eb.HasMany(ur => ur.Users)
                .WithOne(u => u.UserRole)
                .HasForeignKey(u => u.UserRoleId);

               eb.ToTable("UserRoles")
                .HasData(GetRoles()); 
           });

            modelBuilder.Entity<User>(eb =>
            {
               

                eb.HasOne(u => u.Address)
               .WithOne(a => a.User)
               .HasForeignKey<Address>(a => a.UserId);
                eb.ToTable("Users")
               .HasData(GetUser());
            });
                
                
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
               
            };
            return roles;
        }

        private User GetUser()
        {
            User user = new User()
            {
                Id = 1,
                FristName = "Admin",
                LastName = "Admin",
                Email = "admin@local.pl", 
                PhoneNumber ="000000",
                UserRoleId = 2,
                isActive = true
            };
            user.Password = passwordHasher.HashPassword(user, "haslo");
            return user;
        }
    }

}
