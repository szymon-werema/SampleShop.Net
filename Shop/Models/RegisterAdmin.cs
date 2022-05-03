using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Entities;
namespace Shop.Models
{
    public class RegisterAdmin : IRegister<UserAccountAdmin>
    {
        private readonly LocalDBContext context;

        public RegisterAdmin(LocalDBContext localDBContext)
        {
            this.context = localDBContext;
        }
        public  void RegisterUser(UserAccountAdmin user)
        {
            user.SetDefultRole();
            var u = new User()
            {
                Email = user.Email,
                Password = user.Password,
                FristName = user.FristName,
                LastName = user.LastName,
                UserRoleId = user.UserRoleId

            };
             context.User.AddAsync(u);
             context.SaveChangesAsync();


        }
    }
}
