using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Carts;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Portions;

namespace WeLoveFood.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private const int NoAddedPortions = 0;
        private const int InitialPortionsQuantity = 1;

        private readonly WeLoveFoodDbContext _data;
        private readonly IClientsService _clients;
        private readonly IPortionsService _portions;
        private readonly ICartsService _carts;

        public OrdersService(
            WeLoveFoodDbContext data,
            IClientsService clients,
            IPortionsService portions,
            ICartsService carts)
        {
            this._data = data;
            this._clients = clients;
            this._portions = portions;
            this._carts = carts;
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
                .GetClientId(userId);

            if (clientId == null)
            {
                return false;
            }

            return this._data
                .Carts
                .Any(c => c.ClientId == clientId && c.Portions.Any(p => p.Meal.Id == mealId));
        }

        private bool IsMealFromTheSameRestaurant(
            int mealId,
            int restaurantId)
            => this._data
                .Meals
                .Any(m => m.Id == mealId && m.MealsCategory.Restaurant.Id == restaurantId);
    }
}
