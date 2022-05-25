using Microsoft.AspNetCore.Mvc;
using Shop.Models.Forms;
using Shop.Models.AccountMenager;
using Shop.Models.Authenticate;
namespace Shop.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccountMenager accountMenager;
        private readonly IToken<ClaimsLogin> tokenJWT;

        public LoginController(AccountMenager accountMenager,IToken<ClaimsLogin> tokenJWT)
        {
            this.accountMenager = accountMenager;
            this.tokenJWT = tokenJWT;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(new LogInForm());
        }
        [HttpPost]
        public ActionResult Login(LogInForm form)
        {
            if(accountMenager.findUser(form.Email) == null) return View();
            if(!accountMenager.CheackPassword(form.Password, form.Email)) return View();

            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            
            
            return View();
        }

    }
}
