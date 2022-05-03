using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class UserAccountUser : UserAccount
    {


        public override int UserRoleId { get; set; } = 0;
        public override void SetDefultRole()
        {
            this.UserRoleId = 0;
        }

       
    }
}
