
using Microsoft.AspNetCore.Mvc;
using Shop.Models.Register;
using Shop.Models.Forms;
using Shop.Models.Authenticate;
using Shop.Models.Messenger;


namespace Shop.Controllers
{
    
    public class RegisterController : Controller
    {
      
        private readonly IRegister<UserRegisterForm> register;
        private readonly IToken<TokenRegister> tokenJWT;
        private readonly IMessenger<EmailMessageActivation> messenger;

        public RegisterController(IRegister<UserRegisterForm> register, 
            IToken<TokenRegister> tokenJWT,
            IMessenger<EmailMessageActivation> messenger 
            )
        {
            
            this.register = register;
            this.tokenJWT = tokenJWT;
            this.messenger = messenger;
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return BadRequest();
            return View(new UserRegisterForm());
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAsync(UserRegisterForm user)
        {
           
            if (User.Identity.IsAuthenticated) return BadRequest();
            if (ModelState.IsValid)
            {
               
                await register.RegisterAsync(user);
                string token = tokenJWT.generateToken(user.Email);

                Task.Run(() => messenger.sendMessageAsync("To active your account click on link below <br> " + $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + this.Url.Action("RegisterActivate", "Activation", new { token = token }), user.Email));
                
                return View("SuccesRegister");
            }

            ViewBag.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage);
            return View();

        }

    }
}
