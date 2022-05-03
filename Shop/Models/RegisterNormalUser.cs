using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Entities;
namespace Shop.Models
{
    public class RegisterNormalUser: IRegister<UserAccountUser>
    {
        private readonly LocalDBContext context;

        public RegisterNormalUser(LocalDBContext localDBContext)
        {
            this.context = localDBContext;
        }
        public void RegisterUser(UserAccountUser user)
        {
            user.SetDefultRole();
            Console.WriteLine("___");
            Console.WriteLine(user.UserRoleId);
            Console.WriteLine("___");
            var u = new User()
            {
                Email = user.Email,
                Password = user.Password,
                FristName = user.FristName,
                LastName = user.LastName,
                UserRoleId = user.UserRoleId

            };
             context.User.Add(u);
             context.SaveChanges();


        }
    }
}
