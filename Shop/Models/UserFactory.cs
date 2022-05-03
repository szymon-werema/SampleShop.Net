using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Models;
namespace Shop.Models
{
    public class UserFactory : IUserFactory
    {
        public UserAccountAdmin createAdmin()
        {
            return new UserAccountAdmin();
        }
        public UserAccountUser createUser()
        {
            return new UserAccountUser();
        }
    }
}
