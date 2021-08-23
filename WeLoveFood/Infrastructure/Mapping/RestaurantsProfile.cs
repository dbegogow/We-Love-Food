using AutoMapper;
using System.Linq;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Restaurants;
using WeLoveFood.Web.Web.Models.Restaurants;

namespace WeLoveFood.Web.Web.Infrastructure.Mapping
{
    public class RestaurantsProfile : Profile
    {
        private const string WorkingTimeFormat = @"hh\:mm";

        public RestaurantsProfile()
        {
            this.CreateMap<Restaurant, EditRestaurantServiceModel>()
                .ForMember(erf => erf.OpeningTime,
                    cfg =>
                        cfg.MapFrom(ers => ers.OpeningTime.ToString(WorkingTimeFormat)))
                .ForMember(erf => erf.ClosingTime,
                    cfg =>
                        cfg.MapFrom(ers => ers.ClosingTime.ToString(WorkingTimeFormat)));

            this.CreateMap<Restaurant, ManagersRestaurantServiceModel>();

            this.CreateMap<Restaurant, NewRestaurantCardServiceModel>()
                .ForMember(nrc => nrc.MealsCategories,
                    cfg =>
                        cfg.MapFrom(r => r.MealsCategories.Select(mc => mc.Name)));

            this.CreateMap<EditRestaurantServiceModel, EditRestaurantFormModel>();
        }
    }
}
