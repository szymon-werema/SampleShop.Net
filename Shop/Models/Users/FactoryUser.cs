
namespace Shop.Models.Users
{
    public class FactoryUser : IFactoryUser
    {
        public UserAdmin createAdmin()
        {
            return new UserAdmin();
        }

        public UserClient createClient()
        {
            return new UserClient();
        }

        public UserSeller createSeller()
        {
            return new UserSeller();
        }
    }
}
