using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Data.Models;
using System.Collections.Generic;
using WeLoveFood.Services.clients;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Portions
{
    public class PortionsService : IPortionsService
    {
        private readonly WeLoveFoodDbContext _data;
        private readonly IClientsService _clients;

        public PortionsService(
            WeLoveFoodDbContext data,
            IClientsService clients)
        {
            this._data = data;
            this._clients = clients;
        }

        public int RemovePortion(int portionId, string userId)
        {
            var clientId = this._clients
                .GetClientId(userId);

            if (clientId == null)
            {
                return -1;
            }

            var portion = this.GetPortion(portionId, clientId);

            if (portion == null || portion.Quantity == 1)
            {
                return -1;
            }

            portion.Quantity--;

            this._data.SaveChanges();

            return portion.Quantity;
        }

        public int AddPortion(int portionId, string userId)
        {
            var clientId = this._clients
                .GetClientId(userId);

            if (clientId == null)
            {
                return -1;
            }

            var portion = this.GetPortion(portionId, clientId);

            if (portion == null)
            {
                return -1;
            }

            portion.Quantity++;

            this._data.SaveChanges();

            return portion.Quantity;
        }

        public IEnumerable<CartPortionServiceModel> Portions(string clientId)
        {
            return this._data
                .Carts
                .Where(c => c.ClientId == clientId)
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

        public int PortionsCount(string clientId)
            => this._data
                .Carts
                .Where(c => c.ClientId == clientId)
                .Select(c => c.Portions.Count())
                .FirstOrDefault();

        private Portion GetPortion(int portionId, string clientId)
            => this._data
                .Portions
                .FirstOrDefault(p => p.Id == portionId && p.Cart.ClientId == clientId);
    }
}
