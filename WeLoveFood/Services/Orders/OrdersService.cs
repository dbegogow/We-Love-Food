using System;
using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Carts;
using System.Collections.Generic;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Portions;
using WeLoveFood.Services.Restaurants;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private const int NoAddedPortions = 0;
        private const int InitialPortionsQuantity = 1;

        private const string TimeFormat = @"hh\:mm";
        private const string DayFormat = "d";
        private const string PriceFormat = "F2";

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
            var isRestaurantOpen = this._restaurants
                .IsRestaurantOpen(restaurantId);

            if (!isRestaurantOpen)
            {
                return false;
            }

            var clientId = this._clients.ClientId(userId);

            if (clientId == null)
            {
                return false;
            }

            var portionsCount = this._portions
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

        public void MakeOrder(string clientId, decimal totalPrice)
        {
            var cart = this._carts
                .Cart(clientId);

            var portions = this._data
                .Portions
                .Where(p => p.CartId == cart.Id)
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
                Client = client,
                TotalPrice = totalPrice,
                Date = DateTime.Now
            };

            this._data
                .Orders
                .Add(order);

            cart.Portions.Clear();

            this._data.SaveChanges();
        }

        public IEnumerable<ClientOrderServiceModel> ClientOrders(string userId)
        {
            var clientId = this._clients
                .ClientId(userId);

            return this._data
                .Orders
                .Where(o => o.ClientId == clientId)
                .Select(o => new ClientOrderServiceModel
                {
                    Time = o.Date.TimeOfDay.ToString(TimeFormat),
                    Day = o.Date.ToString(DayFormat),
                    RestaurantName = o.Restaurant.Name,
                    RestaurantCityName = o.Restaurant.City.Name,
                    Address = o.Client.User.Address,
                    DeliveryFee = o.Restaurant.DeliveryFee.ToString(),
                    TotalPrice = o.TotalPrice.ToString(PriceFormat),
                    Portions = o.Portions.Select(p => new PortionOrderServiceModel
                    {
                        MealName = p.Meal.Name,
                        Quantity = p.Quantity,
                        Price = p.Meal.Price
                    })
                })
                .ToList();
        }

        private bool IsMealFromTheSameRestaurant(
            int mealId,
            int restaurantId)
            => this._data
                .Meals
                .Any(m => m.Id == mealId && m.MealsCategory.Restaurant.Id == restaurantId);
    }
}
