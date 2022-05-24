using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Entities;
using Shop.Models.Register;
using Shop.Models.Authenticate;
using Shop.Models.Forms;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    
    public class AdminPanelController : Controller
    {
        private readonly LocalDbContext db;
        private readonly IRegister<AddUserForm> register;
        private readonly IToken<TokenByAdmin> tokenJWT;

        public AdminPanelController(LocalDbContext db,
            IRegister<AddUserForm> register,
            IToken<TokenByAdmin> tokenJWT
            )
        {
            this.db = db;
            this.register = register;
            this.tokenJWT = tokenJWT;
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
           
            register.RegisterAsync(user);
            var token = tokenJWT.generateToken(user.Email);
            Console.WriteLine(token);
            return View();
        }

    }
}
