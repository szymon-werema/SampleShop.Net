using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserFactory _userFactory;
        private readonly IRegister<UserAccountUser> _registerAccount;
        public RegisterController(IRegister<UserAccountUser> registerAccount, IUserFactory userFactory)
        {
            _registerAccount = registerAccount;
            _userFactory = userFactory;
        }

        [HttpGet("Register")]
        public ActionResult Register()
        {
            
            return View(_userFactory.createUser());
        }

        [HttpPost("Register")]
        public ActionResult Register(UserAccountUser user)
        {
            
            _registerAccount.RegisterUser(user);
            return View();
            
        }
    }
}
