using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Forms
{
    public class ItemForm
    {
        public string Name { get; set; }
        public int Ammount { get; set; }
        public int Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int Miniature { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
