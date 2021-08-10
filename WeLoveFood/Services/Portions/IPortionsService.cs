using System.Collections.Generic;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Portions
{
    public interface IPortionsService
    {
        int RemovePortion(int portionId, string userId);

        int AddPortion(int portionId, string userId);

        bool DeletePortionFromCart(int portionId, string userId);

        int PortionsCount(string clientId);

        IEnumerable<CartPortionServiceModel> Portions(string clientId);

    }
}
