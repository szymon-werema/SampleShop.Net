
namespace Shop.Models.Forms
{
    public  class UserRegisterForm
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
        public int UserRoleId { get; set; }
    }
}
