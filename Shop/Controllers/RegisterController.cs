using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Models.Register;
using Shop.Models.Users;



namespace Shop.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IFactoryUser factoryUser;
        private readonly IRegister<UserClient> register;

        public RegisterController(IFactoryUser factoryUser, IRegister<UserClient> register)
        {
            this.factoryUser = factoryUser;
            this.register = register;
        }

        [HttpGet("Register")]
        public ActionResult Register()
        {
            
            return View(factoryUser.createClient());
        }

        [HttpPost("Register")]
        public ActionResult Register(UserClient user)
        {
            //foreach (string key in Request.Form.Keys)
            //{
            //    Console.WriteLine(key);

            //}
            if(ModelState.IsValid)
            {
               
                return View();
            }
            //ViewBag.Types= 
            ViewBag.ErrorMessage = ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage);
            //foreach (var m in ViewBag.ErrorMessage)
            //{
            //    Console.WriteLine(m.errorMessage);
            //}

            //foreach( var m in ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage))
            //{
            //    Console.WriteLine(m);
            //}
            return View();
                
            
           
        }
    }
}
