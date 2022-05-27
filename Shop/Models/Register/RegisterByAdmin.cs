using Shop.Models.Forms;
using Shop.Entities;

using Microsoft.AspNetCore.Identity;
namespace Shop.Models.Register
{
    public class RegisterByAdmin : IRegister<AddUserForm>
    {
        private readonly LocalDbContext db;
        
        private readonly IPasswordHasher<User> passwordHasher;

        public RegisterByAdmin(LocalDbContext db ,
            
            IPasswordHasher<User> passwordHasher)
        {
            this.db = db;
            
            this.passwordHasher = passwordHasher;
        }

       

        public async Task RegisterAsync(AddUserForm user)
        {
           
            var newUser = new User()
            {
                Email = user.Email,
                FristName = user.FristName,
                Password = "",
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
               
                UserRoleId = user.UserRoleId
            };
            
            await db.User.AddAsync(newUser);
            await db.SaveChangesAsync();

        }
    }
}
