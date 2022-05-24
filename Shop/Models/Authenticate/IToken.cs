using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Shop.Models.Register;
namespace Shop.Models.Authenticate
{
    public interface IToken <T>
    {
        public string generateToken(string email);
        
        public List<Claim> getClaims(string token);
        public bool veryfyToken(string token);
    }
}
 