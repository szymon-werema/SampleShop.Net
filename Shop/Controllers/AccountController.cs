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
            
            User u = db.User.Where(x => x.Email == email).FirstOrDefault();
           
            if (u == null) BadRequest();
            AccountForm form = new AccountForm()
            {
                User = u,
                Address = db.Address.Where(a => a.UserId == u.Id).FirstOrDefault()
            };
            
            return View(form);
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChangeData(AccountForm u)
        {
            if (!ModelState.IsValid) return RedirectToAction("MyAccount", "Account");
           
            
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
            User u = db.User.Where(x => x.Email == email).FirstOrDefault();
            
           
            if(u == null) return BadRequest();
            form.User= u;
            if (form.User.AddressId != null) form.Address = db.Address.Where(a => a.UserId == u.Id).First();
            return View(form);
            

        }
        [HttpPost]
        [Authorize]
        public ActionResult SetAddress(AccountForm form)
        {
           
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(modelState.ErrorMessage);
                }
                return RedirectToAction("MyAccount", "Account");
            }
            
            if (form.User.AddressId == null)
            {
                Console.WriteLine("_____________________________________________1");
                accountMenager.setAddress(form.User.Email, form.Address);
            }else
            {
                Console.WriteLine("_____________________________________________2");
                accountMenager.updateAddress(form.User.Email, form.Address);
            }
            return RedirectToAction("MyAccount", "Account");
        }


    }
}
