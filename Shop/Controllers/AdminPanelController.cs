using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Entities;
using Shop.Models.Register;
using Shop.Models.Authenticate;
using Shop.Models.Forms;
using Shop.Models.Messenger;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        private readonly LocalDbContext db;
        private readonly IRegister<AddUserForm> register;
        private readonly IToken<TokenByAdmin> tokenJWT;
        private readonly IMessenger<EmailMessageActivation> messenger;

        public AdminPanelController(LocalDbContext db,
            IRegister<AddUserForm> register,
            IToken<TokenByAdmin> tokenJWT,
            IMessenger<EmailMessageActivation> messenger
            )
        {
            this.db = db;
            this.register = register;
            this.tokenJWT = tokenJWT;
            this.messenger = messenger;
        }

        [HttpGet]
        public ActionResult AddUser()
        {

            ViewBag.UserRoleId = new SelectList(db.UserRole.ToDictionary(role => role.Id, role => role.Name),"Key", "Value");
            return View(new AddUserForm());

        }
        [HttpPost]
        public ActionResult AddUser(AddUserForm user)
        {
           if(ModelState.IsValid)
           {
                register.RegisterAsync(user);
                string token = tokenJWT.generateToken(user.Email);
                Task.Run(() => messenger.sendMessageAsync("To active your account click on link below <br> " + $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + this.Url.Action("ActivateAccountPassword", "Activation", new { token = token }), user.Email));
                return View();
           }
           ViewBag.UserRoleId = new SelectList(db.UserRole.ToDictionary(role => role.Id, role => role.Name), "Key", "Value");
           return View();

        }

    }
}
