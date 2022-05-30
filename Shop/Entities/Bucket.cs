using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Bucket
    {
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
