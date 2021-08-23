using System.Collections.Generic;

namespace WeLoveFood.Web.Services.Models.Restaurants
{
    public class RestaurantCardServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImgUrl { get; init; }

        public bool IsOpen { get; init; }

        public List<string> MealsCategories { get; init; }
    }
}