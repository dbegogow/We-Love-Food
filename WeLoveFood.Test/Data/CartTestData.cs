using System.Linq;
using WeLoveFood.Data.Models;

using static WeLoveFood.Test.Data.UsersTestData;
using static WeLoveFood.Test.Data.PortionsTestData;

namespace WeLoveFood.Test.Data
{
    public static class CartTestData
    {
        public static Cart GetCart(int portionsCount)
         => new()
         {
             Client = GetClient(),
             Portions = GetPortions(portionsCount).ToList()
         };
    }
}
