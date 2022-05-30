using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Shop.Entities;
using Shop.Models.Register;


namespace Shop.Models.Authenticate
{
    public class ClaimsAddByAdmin : IClaimsUser<ClaimsAddByAdmin>
    {
        private readonly LocalDbContext db;

        public ClaimsAddByAdmin(LocalDbContext db)
        {
            this.db = db;
        }
        public List<Claim> generateClaims(User user)
        {

            return new List<Claim>()
            {
                 
                 new Claim(ClaimTypes.Authentication, "Activation"),
                 new Claim(ClaimTypes.AuthenticationMethod, "AddedByAdmin"),
                 new Claim(ClaimTypes.Role, db.UserRole
                 .Where(u => u.Id == user.UserRoleId)
                 .Select(r => r.Name).First()),
                 new Claim(ClaimTypes.Email, user.Email.ToString())

            };
        }
    }
}
