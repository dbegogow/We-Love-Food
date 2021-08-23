using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Models.Cities;
using Microsoft.Extensions.Caching.Memory;

using static WeLoveFood.Web.CacheConstants;

namespace WeLoveFood.Web.Components
{
    public class CitiesCardsViewComponent : ViewComponent
    {
        private const int CitiesWithMostRestaurants = 4;

        private readonly IMemoryCache _cache;

        private readonly ICitiesService _cities;

        public CitiesCardsViewComponent(
            IMemoryCache cache,
            ICitiesService cities)
        {
            this._cache = cache;
            this._cities = cities;
        }

        public IViewComponentResult Invoke()
        {
            var citiesCards = this._cache.Get<IEnumerable<CityCardServiceModel>>(CitiesWithMostRestaurantsCacheKey);

            if (citiesCards == null)
            {
                citiesCards = this._cities
                    .CardsOrderByRestaurantsCount(CitiesWithMostRestaurants);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(AbsoluteExpirationMinutes));

                this._cache.Set(CitiesWithMostRestaurantsCacheKey, citiesCards, cacheOptions);
            }

            return View(citiesCards);
        }
    }
}
