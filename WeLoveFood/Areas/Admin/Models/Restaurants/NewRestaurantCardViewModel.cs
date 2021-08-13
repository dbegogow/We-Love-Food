using System.Collections.Generic;

namespace WeLoveFood.Areas.Admin.Models.Restaurants
{
    public class NewRestaurantCardViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string CardImgUrl { get; init; }

        public string CityName { get; init; }

        public List<string> MealsCategories { get; init; }
    }
}