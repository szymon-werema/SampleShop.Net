using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Shop.Models.Register;
namespace Shop.Models.Authenticate
{
    public class TokenLogin : IToken<TokenLogin>
    {
        public string generateToken(string email)
        {
            throw new NotImplementedException();
        }

        public List<Claim> getClaims(string token)
        {
            throw new NotImplementedException();
        }

        public bool veryfyToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
