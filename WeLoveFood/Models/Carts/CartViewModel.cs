using WeLoveFood.Web.Services.Models.Orders;
using WeLoveFood.Web.Services.Models.Users;

namespace WeLoveFood.Web.Web.Models.Carts
{
    public class CartViewModel
    {
        public CartAllPortionsServiceModel CartAllPortions { get; init; }

        public PersonalDataServiceModel PersonalData { get; init; }
    }
}
