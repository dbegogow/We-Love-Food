using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using WeLoveFood.Web.Services.Cities;
using WeLoveFood.Web.Services.Models.Cities;
using static WeLoveFood.Web.CacheConstants;

namespace WeLoveFood.Web.Web.Components
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
