using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ammount { get; set; }
        public int Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        //Owner
        public int UserId { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public DateTime AddedTime { get; set; } = DateTime.Now;
        public List<Image> Images { get; set; } = new List<Image>();
        public int Miniature { get; set; }
        public List<Bucket> Buckets { get; set; } = new List<Bucket>();
        
    }
}
