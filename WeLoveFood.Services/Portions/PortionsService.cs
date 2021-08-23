using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Web.Data;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.clients;
using WeLoveFood.Web.Services.Models.Orders;

namespace WeLoveFood.Web.Services.Portions
{
    public class PortionsService : IPortionsService
    {
        private readonly WeLoveFoodDbContext _data;
        private readonly IConfigurationProvider _mapper;

        private readonly IClientsService _clients;

        public PortionsService(
            WeLoveFoodDbContext data,
            IMapper mapper,
            IClientsService clients)
        {
            this._data = data;
            _mapper = mapper.ConfigurationProvider;

            this._clients = clients;
        }

        public int Remove(int id, string userId)
        {
            var clientId = this._clients
                .GetId(userId);

            if (clientId == null)
            {
                return -1;
            }

            var portion = this.GetPortion(id, clientId);

            if (portion == null || portion.Quantity == 1)
            {
                return -1;
            }

            portion.Quantity--;

            this._data.SaveChanges();

            return portion.Quantity;
        }

        public int Add(int id, string userId)
        {
            var clientId = this._clients
                .GetId(userId);

            if (clientId == null)
            {
                return -1;
            }

            var portion = this.GetPortion(id, clientId);

            if (portion == null)
            {
                return -1;
            }

            portion.Quantity++;

            this._data.SaveChanges();

            return portion.Quantity;
        }

        public bool DeleteFromCart(int id, string userId)
        {
            var clientId = this._clients
                .GetId(userId);

            if (clientId == null)
            {
                return false;
            }

            var portion = this._data
                .Portions
                .FirstOrDefault(p => p.Id == id && p.Cart.ClientId == clientId);

            if (portion == null)
            {
                return false;
            }

            this._data
                .Portions
                .Remove(portion);

            this._data.SaveChanges();

            return true;
        }

        public IEnumerable<CartPortionServiceModel> Portions(string clientId)
        {
            return this._data
                .Carts
                .Where(c => c.ClientId == clientId)
                .SelectMany(c => c.Portions)
                .ProjectTo<CartPortionServiceModel>(this._mapper)
                .ToList();
        }

        public int PortionsCount(string clientId)
            => this._data
                .Carts
                .Where(c => c.ClientId == clientId)
                .Select(c => c.Portions.Count)
                .FirstOrDefault();

        private Portion GetPortion(int portionId, string clientId)
            => this._data
                .Portions
                .FirstOrDefault(p => p.Id == portionId && p.Cart.ClientId == clientId);
    }
}
