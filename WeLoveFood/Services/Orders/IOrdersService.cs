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

        CartAllPortionsServiceModel CartAllPortions(string userId);
    }
}
