using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Entities;
using System.Security.Claims;
using Shop.Models.Forms;
using Shop.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers
{
   
    [Authorize]
    public class ShopController : Controller
    {
        private readonly LocalDbContext db;
        private readonly ShopService shopService;
        private readonly AccountMenager account;


        public ShopController(LocalDbContext db,
            AccountMenager account,
            ShopService shopService
            )
        {
            this.db = db;
            this.account = account;
            this.shopService = shopService;
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
            
            await shopService.addItem(i, id);
           



            return View();
        }
        public ActionResult EditItem()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            var item = db.Items.Where(i => i.Id == id).FirstOrDefault();
            if (item != null)
            {
                db.Remove(item);
                
                db.SaveChanges();
            }

            return RedirectToAction("MyItems", "Shop");
        }
        public ActionResult MyItems()
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            return View(db.Items.Where(i => i.UserId == userId).ToList());
        }
        public async Task<ActionResult> Item(int id)
        {
            //Item i = db.Items.Where(x => x.Id == id).FirstOrDefault();
            //if (i == null) return BadRequest();
            //List<Image> images = db.Images.Where(x => x.ItemId == id).ToList();
            //i.Images = images;
            //i.User = db.User.Where(u => u.Id == i.UserId).First();
            //i.Category = db.Categories.Where(x => x.Id == i.CategoryId).FirstOrDefault();
            Item i  = await db.Items.Where(it => it.Id == id).Include(it => it.Images)
                    .Include(u => u.User)
                    .Include(c => c.Category).FirstAsync();
            if (i == null ) return BadRequest();
           
            return View(i);
        }
        public async Task<ActionResult> AddToCart(int id, int ammount)
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            int bucketId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "BucketId").Value);
           
           
            try
            {
               
                await shopService.addToCart(id, bucketId, ammount);
                return RedirectToAction("Item", "Shop", new { id = id });
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> addCategory()
        {

            return View(new CategoryForm());
        }
        [HttpPost]
        
        public async Task<ActionResult> addCategory(CategoryForm category)
        {
            if(!ModelState.IsValid) return View();
            shopService.addCategory(category);
            return View();
        }
        [HttpGet]
        
        public async Task<ActionResult> addOrder()
        {
            try
            {
                int bucketId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "BucketId").Value);
                await shopService.AddOrder(bucketId);
                return RedirectToAction("ClearBucket", "Account");
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
           
        }
        [HttpGet]
        public async Task<ActionResult> Orders()
        {
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            List<ItemOrder> orders = await shopService.Orders(userId);

            return View(orders);
        }
        [HttpGet]
        public async Task<ActionResult> MyOrders()
        {
            
            ViewBag.SateOrderId = new SelectList(db.StateOrders.ToDictionary(s => s.Id, s => s.Name), "Key", "Value");
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            List<ItemOrder> orders = await shopService.MyOrders(userId);
            if (orders != null) return View();
            return View(orders);
        }
        
        
        [HttpPost]
        public async Task<ActionResult> ChangeState(int stateId, int orderId)
        {
           

            
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                await shopService.ChangeStateOrder(userId, stateId , orderId);
                
                return RedirectToAction("MyOrders", "Shop");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
