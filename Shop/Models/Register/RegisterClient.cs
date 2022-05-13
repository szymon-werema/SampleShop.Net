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
    public class RegisterClient : IRegister<UserClient>
    {
        private readonly LocalDbContext context;
        private readonly IPasswordHasher<User> passwordHasher;

        public RegisterClient(LocalDbContext context, IPasswordHasher<User> passwordHasher )
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }
        public async Task RegisterAsync(UserClient user)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine(user.UserRoleId);
            Console.WriteLine("-----------------------------------------------");
            var newUser = new User()
            {
                Email = user.Email,
                Password = user.Password,
                FristName = user.FristName,
                LastName = user.LastName,
                UserRoleId = user.UserRoleId
            };

            newUser.Password = passwordHasher.HashPassword(newUser, user.Password);
            await context.User.AddAsync(newUser);
            await context.SaveChangesAsync();
        }
    }
}
