using System.Collections.Generic;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Services.Portions
{
    public interface IPortionsService
    {
        int RemovePortion(int portionId, string userId);

        int AddPortion(int portionId, string userId);

        bool DeletePortion(int portionId, string clientId);

        void CreatePortion(string clientId, int mealId);

        int PortionsCount(string clientId);

        int PortionIdByMealId(int mealId);

        IEnumerable<CartPortionServiceModel> Portions(string clientId);

    }
}
