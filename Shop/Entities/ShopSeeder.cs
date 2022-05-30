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

            modelBuilder.Entity<Category>(eb =>
            {
                eb.ToTable("Categories")
                .HasData(GetCategories());
            });
                
            modelBuilder.Entity<User>(eb =>
            {
                eb.HasOne(u => u.Address)
               .WithOne(a => a.User)
               .HasForeignKey<Address>(a => a.UserId);
                eb.ToTable("Users")
               .HasData(GetUser());
            });
            modelBuilder.Entity<Bucket>(eb =>
            {
                eb.ToTable("Buckets").HasData(new Bucket() { Id=1, UserId=1});
            });
        }
        private List<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category(){ Id = 1, Name = "Tables"},
                new Category(){ Id = 2, Name = "Monitors"},
                new Category(){ Id = 3, Name = "Pc"},
                new Category(){ Id = 4, Name = "Books"},
                new Category(){ Id = 5, Name = "Others"}
            };
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
