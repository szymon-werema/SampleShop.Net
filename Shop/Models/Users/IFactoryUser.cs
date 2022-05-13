
namespace Shop.Models.Users
{
    public interface IFactoryUser
    {
        public UserAdmin createAdmin();
        public UserClient createClient();
        public UserSeller createSeller();
    }
}
