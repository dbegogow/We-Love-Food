using System.Collections.Generic;

namespace WeLoveFood.Web.Services.Models.Restaurants
{
    public class NewRestaurantCardServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string CardImgUrl { get; init; }

        public string CityName { get; init; }

        public List<string> MealsCategories { get; init; }
    }
}