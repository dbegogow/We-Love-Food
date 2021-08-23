using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Orders;

namespace WeLoveFood.Web.Services.Carts
{
    public interface ICartsService
    {
        int CartRestaurantId(string clientId);

        Cart Cart(string clientId);

        CartAllPortionsServiceModel AllPortions(string userId);
    }
}
