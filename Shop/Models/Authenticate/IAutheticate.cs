using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Models.Users;
namespace Shop.Models.Authenticate
{
    public interface IAutheticate
    {
        public void autheticateUser(UserForm user);
    }
}
