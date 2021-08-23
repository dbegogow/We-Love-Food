using System;

namespace WeLoveFood.Services.Models.Restaurants
{
    public class RestaurantServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string MainImgUrl { get; init; }

        public decimal? DeliveryFee { get; init; }

        public string OpeningTime { get; init; }

        public string ClosingTime { get; init; }

        public bool IsOpen { get; init; }
    }
}
