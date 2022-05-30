using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class BucketItem
    {
        public int Id { get; set; }
        public int BucketId { get; set; } 
        public Bucket Bucket { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int Ammount { get; set; }  
    }
}
