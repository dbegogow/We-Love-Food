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

        void MakeOrder(string clientId, decimal totalPrice);

        IEnumerable<ClientOrderServiceModel> ClientOrders(string userId);
    }
}
