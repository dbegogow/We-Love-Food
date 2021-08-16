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
            this.CreateMap<Restaurant, RestaurantCardServiceModel>()
                .ForMember(rc => rc.ImgUrl,
                    cfg =>
                        cfg.MapFrom(r => r.CardImgUrl))
                .ForMember(rc => rc.MealsCategories,
                    cfg =>
                        cfg.MapFrom(r => r.MealsCategories.Select(mc => mc.Name)));

            this.CreateMap<Restaurant, NewRestaurantCardViewModel>()
                .ForMember(nrc => nrc.MealsCategories,
                    cfg =>
                        cfg.MapFrom(r => r.MealsCategories.Select(mc => mc.Name)));
        }
    }
}
