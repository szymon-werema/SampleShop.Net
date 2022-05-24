
using Microsoft.AspNetCore.Mvc;
using Shop.Models.Register;
using Shop.Models.Forms;
using Shop.Models.Authenticate;


namespace Shop.Controllers
{
    public class RegisterController : Controller
    {
      
        private readonly IRegister<UserRegisterForm> register;
        private readonly IToken<TokenRegister> tokenJWT;

        public RegisterController(IRegister<UserRegisterForm> register, IToken<TokenRegister> tokenJWT)
        {
            
            this.register = register;
            this.tokenJWT = tokenJWT;
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
               
                var token = tokenJWT.generateToken(user.Email);
                Console.WriteLine(token);
                
                //HttpContext.Session.SetString("Token", token.ToString());
                return View("Views/Register/SuccesRegister.cshtml");
            }

            ViewBag.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage);
            return View();

        }

    }
}
