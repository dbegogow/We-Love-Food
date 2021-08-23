namespace WeLoveFood.Web.Services.Models.Orders
{
    public class CartPortionServiceModel
    {
        public int Id { get; init; }

        public int Quantity { get; init; }

        public decimal Price { get; init; }

        public CartMealServiceModel Meal { get; init; }
    }
}
