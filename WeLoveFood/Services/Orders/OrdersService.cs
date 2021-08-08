using System;
using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.clients;

namespace WeLoveFood.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private const int NoAddedPortions = 0;
        private const int InitialPortionsQuantity = 1;

        private readonly WeLoveFoodDbContext _data;
        private readonly IClientsService _clients;

        public OrdersService(
            WeLoveFoodDbContext data,
            IClientsService clients)
        {
            _data = data;
            _clients = clients;
        }

        public bool AddMealToCart(
            int mealId,
            int restaurantId,
            string userId)
        {
            var clientId = this._clients.GetClientId(userId);

            if (clientId == null)
            {
                return false;
            }

            var portionsCount = this.PortionsCount(clientId);

            if (portionsCount > NoAddedPortions)
            {
                var cartRestaurantId = this.CartRestaurantId(clientId);

                var isMealFromTheSameRestaurant = IsMealFromTheSameRestaurant(mealId, cartRestaurantId);

                if (!isMealFromTheSameRestaurant)
                {
                    return false;
                }
            }

            var portion = new Portion
            {
                MealId = mealId,
                Quantity = InitialPortionsQuantity
            };

            this.Cart(clientId)
                ?.Portions
                .Add(portion);

            this._data.SaveChanges();

            return true;
        }

        public bool IsMealAddedInCart(int mealId, string userId)
        {
            var clientId = this._clients
                .GetClientId(userId);

            if (clientId == null)
            {
                return false;
            }

            return this._data
                .Carts
                .Any(c => c.ClientId == clientId && c.Portions.Any(p => p.Meal.Id == mealId));
        }

        private int CartRestaurantId(string clientId)
            => this.CartQuery(clientId)
                .Select(c => c.Portions
                    .Select(p => p.Meal.MealsCategory.Restaurant.Id)
                    .FirstOrDefault())
                .FirstOrDefault();

        private int PortionsCount(string clientId)
            => this.CartQuery(clientId)
                .Select(c => c.Portions.Count())
                .FirstOrDefault();

        private bool IsMealFromTheSameRestaurant(
            int mealId,
            int restaurantId)
            => this._data
                .Meals
                .Any(m => m.Id == mealId && m.MealsCategory.Restaurant.Id == restaurantId);

        private IQueryable<Cart> CartQuery(string clientId)
            => this._data
                .Carts
                .Where(c => c.ClientId == clientId);

        private Cart Cart(string clientId)
            => this._data
                .Carts
                .FirstOrDefault(c => c.ClientId == clientId);
    }
}
