using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Entities;
namespace Shop.Models.Forms
{
    public class AccountForm
    {
        public User User { get; set; }
        public Address? Address { get; set; }

    }
}
