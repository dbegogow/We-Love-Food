using System.Collections.Generic;

namespace WeLoveFood.Services.Models.Orders
{
    public class ClientOrderServiceModel
    {
        public string Date { get; init; }

        public string RestaurantName { get; init; }

        public string RestaurantCityName { get; init; }

        public string Address { get; init; }

        public string DeliveryFee { get; init; }

        public string TotalPrice { get; init; }

        public IEnumerable<PortionOrderServiceModel> Portions { get; init; }
    }
}
