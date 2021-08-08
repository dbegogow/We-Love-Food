namespace WeLoveFood.Services.Models.Orders
{
    public class CartPortionServiceModel
    {
        public int Id { get; init; }

        public int Quantity { get; init; }

        public CartMealServiceModel Meal { get; init; }
    }
}
