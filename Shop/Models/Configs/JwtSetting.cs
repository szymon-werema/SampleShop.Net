using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Configs
{
    public class JwtSetting
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
       
    }
}
