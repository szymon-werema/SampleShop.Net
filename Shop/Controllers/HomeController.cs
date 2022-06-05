using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LocalDbContext db;

        public HomeController(ILogger<HomeController> logger, 
            LocalDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToDictionary(s => s.Id, s => s.Name), "Key", "Value");
            return View(db.Items.Where(it => it.Ammount>0).Include(it=> it.User).Include(it => it.Images).OrderBy(it => it.AddedTime).ToList());
        }
        public IActionResult Find(string itemName)
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToDictionary(s => s.Id, s => s.Name), "Key", "Value");
            if (itemName != null) return View("Index",db.Items.Where(it => it.Ammount > 0).Where(it => Regex.IsMatch(it.Name, itemName) ).Include(it => it.User).Include(it => it.Images).OrderBy(it => it.AddedTime).ToList());
            return View("Index");
        }
        public IActionResult CategoryFilter(int Category)
        {
            
            ViewBag.CategoryId =new SelectList(db.Categories.ToDictionary(s => s.Id , s => s.Name), "Key", "Value");
            return View("Index", db.Items.Where(it => it.Ammount > 0).Where(it => it.CategoryId == Category).Include(it => it.User).Include(it => it.Images).OrderBy(it => it.AddedTime).ToList());
           
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}