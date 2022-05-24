using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Forms
{
    public class SetPasswordForm
    {
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
