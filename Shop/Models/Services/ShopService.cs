using Shop.Entities;
using Shop.Models.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Models.Services
{
    public class ShopService
    {
        private readonly LocalDbContext db;
        private readonly ImageService imageService;

        public ShopService(LocalDbContext db,
            ImageService imageService)
        {
            this.db = db;
            this.imageService = imageService;
        }
        public async Task addItem(ItemForm i, int userId)
        {
            
            Item item = new Item()
            {
                Name = i.Name,
                Ammount = i.Ammount,
                Price = i.Price,
                ShortDescription = i.ShortDescription,
                Description = i.Description,
                UserId = userId,
                CategoryId = i.CategoryId,
                Miniature = i.Miniature
            };
            await db.Items.AddAsync(item);
            await db.SaveChangesAsync();
            await imageService.addImages(i.Images, item.Id);
            
        }
        public async Task addToCart(int itemId, int bucketId, int ammout)
        {
            Item item = db.Items.Where(x => x.Id == itemId).FirstOrDefault();
            if(item == null) throw new Exception("Bad item ID");
            if(item.Ammount - ammout < 0) throw new Exception("Sold out");
            Bucket b = db.Buckets.Where(x => x.Id == bucketId).First();
            BucketItem bi = db.BucketItems.Where(x => x.ItemId == itemId).Where( x => x.BucketId == bucketId).FirstOrDefault();
            if(bi == null)
            {
                
                
                db.BucketItems.Add(new BucketItem() { Ammount = ammout, ItemId=item.Id, BucketId= bucketId});
            }else
            {
                if(item.Ammount - bi.Ammount - ammout < 0) throw new Exception("Sold out");
                
                bi.Ammount += ammout;
            }
            await db.SaveChangesAsync();
        }
        public async Task addCategory(CategoryForm categoryForm)
        {

            db.Categories.AddAsync(new Category() { Name = categoryForm.Name });
            db.SaveChangesAsync();
        }
       
    }
}
