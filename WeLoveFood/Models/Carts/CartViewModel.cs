using WeLoveFood.Services.Models.Users;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Models.Carts
{
    public class CartViewModel
    {
        public CartAllPortionsServiceModel CartAllPortions { get; init; }

        public PersonalDataServiceModel PersonalData { get; init; }
    }
}
