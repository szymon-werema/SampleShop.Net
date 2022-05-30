using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
