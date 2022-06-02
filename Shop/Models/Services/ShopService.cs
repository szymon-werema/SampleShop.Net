using Microsoft.EntityFrameworkCore;
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
        public async Task AddOrder(int bucketId)
        {
           
            Bucket bucket = db.Buckets.Where(x => x.Id == bucketId)
                .Include(b => b.Items)
                .Include(b => b.User)
                .First();
            List<BucketItem> itemBucket = db.BucketItems.Where(x => x.BucketId == bucketId).Include(it => it.Item).ToList();
           
            foreach ( var item in itemBucket)
            {
                int ordered = item.Ammount;
                item.Item.Ammount = item.Item.Ammount - ordered;
                if (item.Item.Ammount < 0)
                {
                    throw new Exception("Out of stock");
                   
                }

            }
            db.SaveChanges();
            var order = new Order()
            {
                StateOrderId = 1,
                UserId = bucket.UserId,
                Items = bucket.Items

            };
            db.Orders.Add(order);
            db.SaveChanges();
            
           var it = db.ItemOrders.Where(i => i.OrderId == order.Id);
           foreach( var i  in it)
           {
                i.ordered = itemBucket.Where(it => it.Item.Id == i.Item.Id).Select(it => it.Ammount).First();
           }
            
            db.SaveChanges();
        }
        public async Task RemoveCategory(List<Item> items)
        {

        }
        public async Task<List<ItemOrder>> Orders(int userId)
        {
            List<ItemOrder> order = db.ItemOrders.Where(i => i.Order.UserId == userId)
                .Include(i => i.Order)
                .Include(i => i.Order.StateOrder)
                .Include(i => i.Item)
                .Include(i => i.Item.User)
                .Include( i=> i.Item.Images)
                .ToList();
            
            return order;
        }
        public async Task<List<ItemOrder>> MyOrders(int userId)
        {
           
            List<ItemOrder> order = db.ItemOrders
                .Where(i => i.Item.UserId == userId)
                .Include(i => i.Item)
                .Include(i => i.Order)
                .Include(i => i.Order.User)
                .Include(i => i.Order.StateOrder)
                .Include(i => i.Item)
                .ToList();
            return order;
        }
        public async Task ChangeStateOrder(int userId, int stateId, int orderId)
        {

            var order = db.Orders.Where(o => o.Id == orderId).Where(o => o.Items.Any( i => i.UserId == userId) == true).FirstOrDefault();
            if (order == null) throw new Exception("Not found order");
            StateOrder state = db.StateOrders.Where(s => s.Id == stateId).FirstOrDefault();
            if (state == null) throw new Exception("Not found state");
            order.StateOrder = state;
            
            db.SaveChanges();
            
        }
    }
}
