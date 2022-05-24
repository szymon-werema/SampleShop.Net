using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Shop.Entities;
namespace Shop.Models.Authenticate
{
    public interface IClaimsUser <T>
    {
        public List<Claim> generateClaims(User user);
    }
}
