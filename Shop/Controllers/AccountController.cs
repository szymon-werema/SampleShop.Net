using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.Models.Forms;
using System.Security.Claims;
using System.Security.Principal;
using Shop.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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

            User u = accountMenager.findUser(email);
           
            if (u == null) return BadRequest();
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
        public async Task<ActionResult> FindUser(string email)
        {
            if (User.Identity.IsAuthenticated && HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value == email) return RedirectToAction("MyAccount", "Account");
            AccountForm form = new AccountForm();
            User u = accountMenager.findUser(email);
            
           
            if(u == null) return BadRequest();
            u = await db.User.Include(a => a.Address).FirstAsync();
            form.User= u;
            if (form.User.Address != null) form.Address = form.User.Address;
            return View(form);
            

        }
        [HttpPost]
        [Authorize]
        public ActionResult SetAddress(AccountForm form)
        {
           
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyAccount", "Account");
            }
            
            if (form.User.Address == null)
            {
                
                accountMenager.setAddress(form.User.Email, form.Address);
            }else
            {
                
                accountMenager.updateAddress(form.User.Email, form.Address);
            }
            return RedirectToAction("MyAccount", "Account");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Bucket()
        {
            int bucketId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "BucketId").Value);
            Bucket b = await accountMenager.getBucket(bucketId);
            return View(b);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> RemoveItem(int itemid)
        {
            int bucketId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "BucketId").Value);
            accountMenager.deleteBucket(itemid, bucketId);
            return RedirectToAction("Bucket", "Account");
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ClearBucket()
        {
            int bucketId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "BucketId").Value);
            accountMenager.clearBucket(bucketId);
            return RedirectToAction("Bucket", "Account");
        }
    }
}
