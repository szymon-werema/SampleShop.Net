using Microsoft.AspNetCore.Identity;
using Shop.Entities;
using Shop.Models.Forms;

namespace Shop.Models.Register
{
    public class RegisterClient : IRegister<UserRegisterForm>
    {
        private readonly LocalDbContext db;
        private readonly IPasswordHasher<User> passwordHasher;


        public RegisterClient(LocalDbContext db, IPasswordHasher<User> passwordHasher)
        {
            this.db = db;
            this.passwordHasher = passwordHasher;
        }

        public async Task RegisterAsync(UserRegisterForm user)
        {
            
            var newUser = new User()
            {
                Email = user.Email,
                Password = user.Password,
                FristName = user.FristName,
                LastName = user.LastName,
                UserRoleId = 1
            };

            newUser.Password = passwordHasher.HashPassword(newUser, user.Password);
            await db.User.AddAsync(newUser);
            await db.SaveChangesAsync(); 
        }
        
      
    }
}
