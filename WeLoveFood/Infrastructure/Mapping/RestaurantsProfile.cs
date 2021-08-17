using AutoMapper;
using System.Linq;
using WeLoveFood.Data.Models;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Infrastructure.Mapping
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            this.CreateMap<Restaurant, EditRestaurantServiceModel>();

            this.CreateMap<Restaurant, ManagersRestaurantServiceModel>();

            this.CreateMap<Restaurant, NewRestaurantCardViewModel>()
                .ForMember(nrc => nrc.MealsCategories,
                    cfg =>
                        cfg.MapFrom(r => r.MealsCategories.Select(mc => mc.Name)));

            this.CreateMap<EditRestaurantServiceModel, RestaurantFormModel>();
        }
    }
}
