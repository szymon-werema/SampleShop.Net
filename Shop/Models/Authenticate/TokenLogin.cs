using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Shop.Entities;
using Shop.Models.Configs;
using Shop.Models.Register;

namespace Shop.Models.Authenticate
{
    public class TokenLogin : IToken<TokenLogin>
    {
        private readonly JwtSetting jwtSettings;
        private readonly LocalDbContext db;
        private readonly IClaimsUser<ClaimsLogin> claim;

        public TokenLogin(JwtSetting jwtSettings, LocalDbContext db, IClaimsUser<ClaimsLogin> claim)
        {
            this.jwtSettings = jwtSettings;
            this.db = db;
            this.claim = claim;
        }
        public string generateToken(string email)
        {
            var claims = claim.generateClaims(db.User.Where(u => u.Email == email).First());
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(jwtSettings.JwtExpireDays);
            var token = new JwtSecurityToken(

                claims: claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public List<Claim> getClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtSettings.JwtKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims.ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool veryfyToken(string token)
        {
            if (token == null) return false;
            var claims = getClaims(token);
            if (claims == null) return false;
            if (claims.FirstOrDefault(x => x.Type == ClaimTypes.Authentication).Value == "Login")
            {
                return true;
            }
            return false;
        }
    }
}
