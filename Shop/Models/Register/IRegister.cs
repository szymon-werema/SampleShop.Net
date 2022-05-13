using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Models.Users;
namespace Shop.Models.Register
{
    public interface IRegister <T>
    {
       public Task RegisterAsync(T user);
    }
}
