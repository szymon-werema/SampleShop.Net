using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Authenticate
{
    public class ClaimsLogin : IClaimsUser<ClaimsLogin>
    {
        private readonly LocalDbContext db;

        public ClaimsLogin(LocalDbContext db)
        {
            this.db = db;
        }
        public List<Claim> generateClaims(User user)
        {
            return new List<Claim>()
            {

                 new Claim(ClaimTypes.Authentication, "Login"),
                 new Claim(ClaimTypes.Role, db.UserRole
                 .Where(u => u.Id == user.UserRoleId)
                 .Select(r => r.Name).First()),
                 new Claim(ClaimTypes.Email, user.Email.ToString()),
                 new Claim(ClaimTypes.Name, user.FristName),
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim("LastName", user.FristName),
                 new Claim("BucketId", db.Buckets.Where( b => b.UserId == user.Id).Select(b => b.Id).First().ToString())
            };
        }
    }
}
