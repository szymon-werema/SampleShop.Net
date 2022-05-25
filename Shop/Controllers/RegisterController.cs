
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

        [HttpGet("Register")]
        public ActionResult Register()
        {
            
            return View("Views/Register/Register.cshtml",new UserRegisterForm());
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync(UserRegisterForm user)
        {
            //foreach (string key in Request.Form.Keys)
            //{
            //    Console.WriteLine(key);

            //}
            if(ModelState.IsValid)
            {
                
                await register.RegisterAsync(user);
                string token = tokenJWT.generateToken(user.Email);
                
                await messenger.sendMessageAsync("To active your account click on link below <br> " + $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + this.Url.Action("RegisterActivate", "Activation", new { token = token }), "lolxwot@gmail.com");
                return View("Views/Register/SuccesRegister.cshtml");
            }

            ViewBag.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage);
            return View();

        }

    }
}
