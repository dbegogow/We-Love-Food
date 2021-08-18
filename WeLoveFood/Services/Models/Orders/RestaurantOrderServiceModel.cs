using System.Collections.Generic;

namespace WeLoveFood.Services.Models.Orders
{
    public class RestaurantOrderServiceModel
    {
        public string Time { get; init; }

        public string Day { get; init; }

        public string Address { get; init; }

        public string TotalPrice { get; init; }

        public bool IsAccepted { get; init; }

        public IEnumerable<PortionOrderServiceModel> Portions { get; init; }
    }
}
