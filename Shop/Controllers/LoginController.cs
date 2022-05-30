using Microsoft.AspNetCore.Mvc;
using Shop.Models.Forms;
using Shop.Models.Services;
using Shop.Models.Authenticate;
using Shop.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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
            if (User.Identity.IsAuthenticated) return BadRequest();

            return View(new LogInForm());
        }
        [HttpPost]
        public async Task<ActionResult> Login(LogInForm form)
        {
            if (User.Identity.IsAuthenticated) return BadRequest();
            if (!ModelState.IsValid) return View();
            
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



            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> LogOut()
        {
            if (!User.Identity.IsAuthenticated) return BadRequest();
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
