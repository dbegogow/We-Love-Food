using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WeLoveFood.Services.Cities;
using WeLoveFood.Services.Models.Cities;
using Microsoft.Extensions.Caching.Memory;
using WeLoveFood.Web.Infrastructure.Extensions;

using static WeLoveFood.Web.CacheConstants;

namespace WeLoveFood.Web.Controllers
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
            IEnumerable<CityCardServiceModel> citiesCards = null;

            if (User.IsAdmin())
            {
                citiesCards = this._cities
                    .CardsOrderByRestaurantsCount();
            }
            else
            {
                citiesCards = this._cache.Get<IEnumerable<CityCardServiceModel>>(AllCitiesCardsCacheKey);

                if (citiesCards == null)
                {
                    citiesCards = this._cities
                        .CardsOrderByRestaurantsCount();

                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(AbsoluteExpirationMinutes));

                    this._cache.Set(AllCitiesCardsCacheKey, citiesCards, cacheOptions);
                }
            }

            return View(citiesCards);
        }
    }
}
