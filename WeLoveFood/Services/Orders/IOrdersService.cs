using System.Collections.Generic;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Orders
{
    public interface IOrdersService
    {
        bool AddMealToCart(
            int mealId,
            int restaurantId,
            string userId);

        bool IsMealAddedInCart(int mealId, string userId);

        void Make(string clientId, string address,decimal totalPrice);

        void Accept(int orderId);

        IEnumerable<RestaurantOrderServiceModel> RestaurantOrders(int restaurantId);

        IEnumerable<ClientOrderServiceModel> ClientOrders(string userId);
    }
}
