using System;

namespace WeLoveFood.Models.Restaurants
{
    public class RestaurantCardViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ImgUrl { get; init; }

        public TimeSpan OpeningTime { get; set; }

        public TimeSpan ClosingTime { get; set; }
    }
}
