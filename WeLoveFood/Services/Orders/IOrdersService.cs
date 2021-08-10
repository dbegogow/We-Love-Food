namespace WeLoveFood.Services.Orders
{
    public interface IOrdersService
    {
        bool AddMealToCart(
            int mealId,
            int restaurantId,
            string userId);

        bool IsMealAddedInCart(int mealId, string userId);

        void MakeOrder(string clientId);
    }
}
