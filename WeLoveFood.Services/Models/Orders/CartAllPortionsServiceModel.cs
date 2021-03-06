using System.Collections.Generic;

namespace WeLoveFood.Services.Models.Orders
{
    public class CartAllPortionsServiceModel
    {
        public decimal TotalPrice { get; init; }

        public decimal DeliveryFee { get; init; }

        public IEnumerable<CartPortionServiceModel> Portions { get; init; }
    }
}
