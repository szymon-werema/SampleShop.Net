using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public string Name { get; set; }
    }
}
