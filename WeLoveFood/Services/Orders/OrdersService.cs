using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using System.Collections.Generic;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Models.Orders;
using WeLoveFood.Services.Restaurants;

namespace WeLoveFood.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private const int NoAddedPortions = 0;
        private const int InitialPortionsQuantity = 1;

        private readonly WeLoveFoodDbContext _data;
        private readonly IClientsService _clients;
        private readonly IRestaurantsService _restaurants;

        public OrdersService(
            WeLoveFoodDbContext data,
            IClientsService clients,
            IRestaurantsService restaurants)
        {
            _data = data;
            _clients = clients;
            _restaurants = restaurants;
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

        public CartAllPortionsServiceModel CartAllPortions(string userId)
        {
            var clientId = this._clients
                .GetClientId(userId);

            var portions = this.Portions(clientId).ToList();
            var totalPrice = portions.Sum(p => p.Price);

            var cartRestaurantId = this.CartRestaurantId(clientId);
            var deliveryFee = this._restaurants
                .DeliveryFee(cartRestaurantId);

            return new CartAllPortionsServiceModel
            {
                Portions = portions,
                TotalPrice = totalPrice,
                DeliveryFee = deliveryFee
            };
        }

        private IEnumerable<CartPortionServiceModel> Portions(string clientId)
        {
            return this.CartQuery(clientId)
                .SelectMany(c => c.Portions)
                .Select(p => new CartPortionServiceModel
                {
                    Id = p.Id,
                    Quantity = p.Quantity,
                    Price = p.Meal.Price * p.Quantity,
                    Meal = new CartMealServiceModel
                    {
                        Id = p.Meal.Id,
                        ImgUrl = p.Meal.ImgUrl,
                        Name = p.Meal.Name,
                        Price = p.Meal.Price
                    }
                })
                .ToList();
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
