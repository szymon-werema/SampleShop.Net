using Microsoft.AspNetCore.Mvc;
using Shop.Models.Authenticate;
using Shop.Models.AccountMenager;
using System.Security.Claims;
using Shop.Entities;
using Shop.Models.Forms;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    
    public class ActivationController : Controller
    {
        private readonly IToken<TokenByAdmin> tokenJWT;
        private readonly LocalDbContext db;
        private readonly AccountMenager accountMenager;

        public ActivationController(
            IToken<TokenByAdmin> tokenJWT, 
            LocalDbContext db,
            AccountMenager accountMenager
 
            )
        {
            this.tokenJWT = tokenJWT;
            this.db = db;
            this.accountMenager = accountMenager;
        }
        [HttpGet]
        public ActionResult RegisterActivate(string token)
        {
            

            if (!tokenJWT.veryfyToken(token)) return View("BadToken");
            List<Claim> claims = tokenJWT.getClaims(token);
            string email = claims.Find(x => x.Type == ClaimTypes.Email).Value;
            if (accountMenager.CheackActivation(email)) return View("AlreadyActivated");
            if (claims.Any(x => x.Type == ClaimTypes.AuthenticationMethod)) return RedirectToAction("ActivateAccountPassword", "Activation", new { token = token });
            accountMenager.ActiveAccountAsync(claims.Find(x => x.Type == ClaimTypes.Email).Value);
            return View("SuccesActivation");

        }
        [HttpGet]
        public ActionResult ActivateAccountPassword(string token)
        {
            if (!tokenJWT.veryfyToken(token)) return View("BadToken");
            List<Claim> claims = tokenJWT.getClaims(token);
            Console.WriteLine(claims.Any(x => x.Type == ClaimTypes.AuthenticationMethod));
            if (!claims.Any(x => x.Type == ClaimTypes.AuthenticationMethod)) return RedirectToAction("RegisterActivate", "Activation", new { token = token });
            string email = claims.Find(x => x.Type == ClaimTypes.Email).Value;
            if (accountMenager.CheackActivation(email)) return View("AlreadyActivated");
            return View(new SetPasswordForm() { Token = token});
        }
        [HttpPost]
        public ActionResult ActivateAccountPassword(SetPasswordForm form)
        {
            if(ModelState.IsValid)
            {
                if (!tokenJWT.veryfyToken(form.Token)) return View("BadToken");
                List<Claim> claims = tokenJWT.getClaims(form.Token);
                if (!claims.Any(x => x.Type == ClaimTypes.AuthenticationMethod)) return RedirectToAction("RegisterActivate", "Activation", new { token = form.Token });
                string email = claims.Find(x => x.Type == ClaimTypes.Email).Value;
                if (accountMenager.CheackActivation(email)) return View("AlreadyActivated");
                accountMenager.ChangePasswordAsync(email, form.Password);
                accountMenager.ActiveAccountAsync(email);
                return View("SuccesActivation");
            }

            return View();
        }


    }
}
