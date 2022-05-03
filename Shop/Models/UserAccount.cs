using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public abstract class UserAccount
    {
        public  string FristName { get; set; }
        public  string LastName { get; set; } 
        public  string Email { get; set; } 
        public  string Password { get; set; }
        public abstract int UserRoleId { get; set; }

        public abstract void SetDefultRole();
       
    }
}
