using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Carts
{
    public interface ICartsService
    {
        int CartRestaurantId(string clientId);

        Cart Cart(string clientId);

        CartAllPortionsServiceModel AllPortions(string userId);
    }
}
