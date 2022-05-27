using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.Models.Forms;
using System.Security.Claims;
using System.Security.Principal;
using Shop.Models.AccountMenager;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{

    public class AccountController : Controller
    {
        private readonly LocalDbContext db;
        private readonly AccountMenager accountMenager;

        public AccountController(LocalDbContext db,
            AccountMenager accountMenager
            )
        {
            this.db = db;
            this.accountMenager = accountMenager;
        }
        [HttpGet]
        [Authorize]
        public ActionResult MyAccount()
        {
            string email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            AccountForm form = new AccountForm();
            form.User = db.User.Where(x => x.Email == email).FirstOrDefault();
            if(form.User.Address!=null) form.Address = form.User.Address;
            return View(form);
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangeData(AccountForm u)
        {
            string email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            accountMenager.changeLastName(email, u.User.LastName);
            accountMenager.changeFirstName(email, u.User.FristName);
            accountMenager.changePhonenumber(email, u.User.PhoneNumber);

            return RedirectToAction("MyAccount", "Account");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult FindUser(string email)
        {
            if (User.Identity.IsAuthenticated && HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value == email) return RedirectToAction("MyAccount", "Account");
            AccountForm form = new AccountForm();
            form.User = db.User.Where(x => x.Email == email).FirstOrDefault();
            if(form.User == null) return BadRequest();
            if (form.User.Address != null) form.Address = form.User.Address;
            return View(form);
            

        }
        [HttpPost]
        [Authorize]
        public ActionResult SetAddress(Address address)
        {
            Console.WriteLine("ZAPISUJE");
            return RedirectToAction("MyAccount", "Account");
        }


    }
}
