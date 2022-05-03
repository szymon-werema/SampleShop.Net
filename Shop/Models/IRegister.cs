using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public interface IRegister<T>
    {
        void RegisterUser(T user);
    }
}
