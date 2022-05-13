using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Models.Users;
using Shop.Entities;
namespace Shop.Models.Register
{
    public class RegisterSeller: IRegister<UserSeller>
    {
        private readonly LocalDbContext context;
        private readonly IPasswordHasher<User> passwordHasher;

        public RegisterSeller(LocalDbContext context, IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }
        public async Task RegisterAsync(UserSeller user)
        {
            User newUser = new User()
            {
                Email = user.Email,
                Password = user.Password,
                FristName = user.FristName,
                LastName = user.LastName,
            };
            newUser.Password = passwordHasher.HashPassword(newUser, newUser.Password);
            await context.User.AddAsync(newUser);
            await context.SaveChangesAsync();
        }
    }
}
