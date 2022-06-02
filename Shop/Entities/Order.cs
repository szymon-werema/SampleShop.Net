using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int StateOrderId { get; set; }
        public StateOrder StateOrder { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Item> Items { get; set; }
        public DateTime Ordered { get; set; } = DateTime.Now;
        
    }
}
