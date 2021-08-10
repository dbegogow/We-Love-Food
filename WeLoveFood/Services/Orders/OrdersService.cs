using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Carts;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Portions;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private const int NoAddedPortions = 0;
        private const int InitialPortionsQuantity = 1;

        private readonly WeLoveFoodDbContext _data;

        private readonly ICartsService _carts;
        private readonly IClientsService _clients;
        private readonly IPortionsService _portions;
        private readonly IRestaurantsService _restaurants;

        public OrdersService(
            WeLoveFoodDbContext data,
            ICartsService carts,
            IClientsService clients,
            IPortionsService portions,
            IRestaurantsService restaurants)
        {
            this._data = data;

            this._carts = carts;
            this._clients = clients;
            this._portions = portions;
            this._restaurants = restaurants;
        }

        public bool AddMealToCart(
            int mealId,
            int restaurantId,
            string userId)
        {
            var clientId = this._clients.ClientId(userId);

            if (clientId == null)
            {
                return false;
            }

            var portionsCount = this
                ._portions
                .PortionsCount(clientId);

            if (portionsCount > NoAddedPortions)
            {
                var cartRestaurantId = this._carts
                    .CartRestaurantId(clientId);

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

            this._carts
                .Cart(clientId)
                ?.Portions
                .Add(portion);

            this._data.SaveChanges();

            return true;
        }

        public bool IsMealAddedInCart(int mealId, string userId)
        {
            var clientId = this._clients
                .ClientId(userId);

            if (clientId == null)
            {
                return false;
            }

            return this._data
                .Carts
                .Any(c => c.ClientId == clientId && c.Portions.Any(p => p.Meal.Id == mealId));
        }

        public void MakeOrder(string clientId)
        {
            var cart = this._carts
                .Cart(clientId);

            var portions = this._data
                .Portions
                .Where(c => c.CartId == cart.Id)
                .ToList();

            var restaurantId = this._carts
                .CartRestaurantId(clientId);

            var restaurant = this._restaurants
                .Restaurant(restaurantId);

            var client = this._data
                .Clients
                .FirstOrDefault(c => c.Id == clientId);

            var order = new Order
            {
                Restaurant = restaurant,
                Portions = portions,
                Client = client
            };

            this._data
                .Orders
                .Add(order);

            cart.Portions.Clear();

            this._data.SaveChanges();
        }

        private bool IsMealFromTheSameRestaurant(
            int mealId,
            int restaurantId)
            => this._data
                .Meals
                .Any(m => m.Id == mealId && m.MealsCategory.Restaurant.Id == restaurantId);
    }
}
