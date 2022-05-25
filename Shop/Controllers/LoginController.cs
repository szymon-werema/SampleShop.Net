using Microsoft.AspNetCore.Mvc;
using Shop.Models.Forms;
using Shop.Models.AccountMenager;
using Shop.Models.Authenticate;
using Shop.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace Shop.Controllers
{
    
    
    public class LoginController : Controller
    {
        private readonly AccountMenager accountMenager;
       
        private readonly IClaimsUser<ClaimsLogin> claim;
        private readonly LocalDbContext db;

        public LoginController(AccountMenager accountMenager,
            
            IClaimsUser<ClaimsLogin> claim,
            LocalDbContext db
            )
        {
            this.accountMenager = accountMenager;
            
            this.claim = claim;
            this.db = db;
        }
        [HttpGet]
        
        public ActionResult Login()
        {
            return View(new LogInForm());
        }
        [HttpPost]
        public async Task<ActionResult> Login(LogInForm form)
        {
            if (accountMenager.findUser(form.Email) == null)
            {
                ViewBag.ErrorMessage = "Bad login or password";
                return View();
            }
            
            if(!accountMenager.CheackPassword(form.Password, form.Email))
            {
                ViewBag.ErrorMessage = "Bad login or password";
                return View();
            }
            if(!accountMenager.CheackActivation(form.Email))
            {
                ViewBag.ErrorMessage = "Your account is not activated";
                return View();
            }
            var claims = claim.generateClaims(db.User.Where(u => u.Email == form.Email).FirstOrDefault());

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                });



            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            
            
            return View();
        }

    }
}
