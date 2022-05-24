
namespace Shop.Models.Register
{
    public interface IRegister <T>
    {
       public Task RegisterAsync(T user);
       

    }
}
