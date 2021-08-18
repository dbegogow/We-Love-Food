using System.Collections.Generic;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Portions
{
    public interface IPortionsService
    {
        int Remove(int id, string userId);

        int Add(int id, string userId);

        bool DeleteFromCart(int id, string userId);

        int PortionsCount(string clientId);

        IEnumerable<CartPortionServiceModel> Portions(string clientId);

    }
}
