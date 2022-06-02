
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole? UserRole { get; set; }
        public int UserRoleId { get; set; }
        public bool isActive { get; set; } = false;
        public Address? Address { get; set; } =null;
        public string PhoneNumber { get; set; }
        public List<Item> Item { get; set; } =new List<Item>();
        public Bucket? Bucket { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        
       
    }
}
