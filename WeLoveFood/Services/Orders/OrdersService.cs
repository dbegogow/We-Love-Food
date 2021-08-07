using System;
using WeLoveFood.Data;

namespace WeLoveFood.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly WeLoveFoodDbContext _data;

        public OrdersService(WeLoveFoodDbContext data)
            => _data = data;

        public bool AddMealToCart(int mealId)
        {
            throw new NotImplementedException();
        }
    }
}
