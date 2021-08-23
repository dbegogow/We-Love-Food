﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeLoveFood.Web.Services.Models.Restaurants;

namespace WeLoveFood.Web.Web.Models.Restaurants
{
    public class AllCityRestaurantsCardsQueryModel
    {
        public const int RestaurantsPerPage = 16;

        [Display(Name = "Търси ресторант")]
        public string SearchTerm { get; init; }

        public int CurrentPage { get; set; } = 1;

        public int TotalRestaurants { get; set; }

        public string CityName { get; set; }

        public IEnumerable<RestaurantCardServiceModel> RestaurantsCards { get; set; }
    }
}