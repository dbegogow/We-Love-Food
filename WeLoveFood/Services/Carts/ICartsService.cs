using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Carts
{
    public interface ICartsService
    {
        CartAllPortionsServiceModel CartAllPortions(string userId);

        int CartRestaurantId(string clientId);

        Cart Cart(string clientId);
    }
}
