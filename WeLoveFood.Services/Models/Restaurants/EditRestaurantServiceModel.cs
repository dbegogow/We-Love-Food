namespace WeLoveFood.Web.Services.Models.Restaurants
{
    public class EditRestaurantServiceModel
    {
        public string Name { get; init; }

        public decimal? DeliveryFee { get; init; }

        public string OpeningTime { get; init; }

        public string ClosingTime { get; init; }

        public string CityName { get; init; }
    }
}
