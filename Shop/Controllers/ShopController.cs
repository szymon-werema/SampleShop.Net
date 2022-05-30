using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Entities;
using System.Security.Claims;
using Shop.Models.Forms;
using Shop.Models.Services;

namespace Shop.Controllers
{
   
    [Authorize]
    public class ShopController : Controller
    {
        private readonly LocalDbContext db;
        private readonly ShopService itemService;
        private readonly AccountMenager account;
       

        public ShopController(LocalDbContext db,
            AccountMenager  account,
            ShopService itemService
            )
        {
            this.db = db;
            this.account = account;
            this.itemService = itemService;
        }

        [HttpGet]
        public ActionResult AddItem()
        {
            string email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            
            ViewBag.CategoryId = new SelectList(db.Categories.ToDictionary(category => category.Id, category => category.Name), "Key", "Value");

            return View(new ItemForm());
        }
        
        [HttpPost]
        public async Task<ActionResult> AddItem(ItemForm i)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.CategoryId = new SelectList(db.Categories.ToDictionary(category => category.Id, category => category.Name), "Key", "Value");
                return View(i);
            }
            int id = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            
            await itemService.addItem(i, id);
           



            return View();
        }
        public ActionResult EditItem()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }
        public ActionResult Item(int id)
        {
            Item i = db.Items.Where(x => x.Id == id).FirstOrDefault();
            if(i == null) return BadRequest();
            List<Image> images = db.Images.Where(x => x.ItemId == id).ToList();
            i.Images = images;
            i.User = db.User.Where(u => u.Id == i.UserId).First();
            i.Category = db.Categories.Where(x => x.Id == i.CategoryId).FirstOrDefault();
            return View(i);
        }
        public async Task<ActionResult> AddToCart(int id, int ammount)
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            int bucketId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "BucketId").Value);
          
            
           
            try
            {
                await itemService.addToCart(id, 2,ammount);
                return RedirectToAction("Item", "Shop", new { id = id });
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);
            }
                
                
           

           

        }
    }
}
