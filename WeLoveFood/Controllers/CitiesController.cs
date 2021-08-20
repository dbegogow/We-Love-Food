using System;
using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;
using System.Collections.Generic;
using WeLoveFood.Services.Models.Cities;
using Microsoft.Extensions.Caching.Memory;

using static WeLoveFood.CacheConstants;

namespace WeLoveFood.Controllers
{
    public class CitiesController : Controller
    {
        private readonly IMemoryCache _cache;

        private readonly ICitiesService _cities;

        public CitiesController(
            IMemoryCache cache,
            ICitiesService cities)
        {
            this._cache = cache;
            this._cities = cities;
        }

        public IActionResult All()
        {
            var citiesCards = this._cache.Get<IEnumerable<CityCardServiceModel>>(AllCitiesCardsCacheKey);

            if (citiesCards == null)
            {
                citiesCards = this._cities
                   .CardsOrderByRestaurantsCount();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(AbsoluteExpirationMinutes));

                this._cache.Set(AllCitiesCardsCacheKey, citiesCards, cacheOptions);
            }

            return View(citiesCards);
        }
    }
}
