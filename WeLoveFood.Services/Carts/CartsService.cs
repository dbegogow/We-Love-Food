using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Portions;
using WeLoveFood.Services.Restaurants;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Carts
{
    public class CartsService : ICartsService
    {
        private readonly WeLoveFoodDbContext _data;

        private readonly IClientsService _clients;
        private readonly IPortionsService _portions;
        private readonly IRestaurantsService _restaurants;

        public CartsService(
            WeLoveFoodDbContext data,
            IClientsService clients,
            IPortionsService portions,
            IRestaurantsService restaurants)
        {
            this._data = data;

            this._clients = clients;
            this._portions = portions;
            this._restaurants = restaurants;
        }

        public int CartRestaurantId(string clientId)
            => this._data
                .Carts
                .Where(c => c.ClientId == clientId)
                .Select(c => c.Portions
                    .Select(p => p.Meal.MealsCategory.Restaurant.Id)
                    .FirstOrDefault())
                .FirstOrDefault();

        public Cart Cart(string clientId)
            => this._data
                .Carts
                .FirstOrDefault(c => c.ClientId == clientId);

        public CartAllPortionsServiceModel AllPortions(string userId)
        {
            var clientId = this._clients
                .GetId(userId);

            var cartRestaurantId = this.CartRestaurantId(clientId);
            var deliveryFee = this._restaurants
                .DeliveryFee(cartRestaurantId);

            var portions = this._portions
                .Portions(clientId).ToList();
            var totalPrice = portions.Sum(p => p.Price) + deliveryFee;

            return new CartAllPortionsServiceModel
            {
                Portions = portions,
                TotalPrice = totalPrice,
                DeliveryFee = deliveryFee
            };
        }
    }
}
