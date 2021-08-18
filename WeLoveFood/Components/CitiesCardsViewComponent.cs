using Microsoft.AspNetCore.Mvc;
using WeLoveFood.Services.Cities;

namespace WeLoveFood.Components
{
    public class CitiesCardsViewComponent : ViewComponent
    {
        private const int CitiesWithMostRestaurants = 4;

        private readonly ICitiesService _cities;

        public CitiesCardsViewComponent(ICitiesService cities)
            => _cities = cities;

        public IViewComponentResult Invoke()
        {
            var cities = this._cities
                .CardsOrderByRestaurantsCount(CitiesWithMostRestaurants);

            return View(cities);
        }
    }
}
