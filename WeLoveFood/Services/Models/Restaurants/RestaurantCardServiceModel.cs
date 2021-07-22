using System;

namespace WeLoveFood.Services.Models.Restaurants
{
    public class RestaurantCardServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImgUrl { get; init; }

        public bool IsOpen { get; init; }
    }
}