﻿
namespace Shop.Models.Forms
{
    public class AddUserForm
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserRoleId { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
