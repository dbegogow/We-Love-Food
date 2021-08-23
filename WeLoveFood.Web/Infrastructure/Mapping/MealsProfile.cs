using AutoMapper;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Menus;

namespace WeLoveFood.Web.Infrastructure.Mapping
{
    public class MealsProfile : Profile
    {
        public MealsProfile()
        {
            this.CreateMap<MealsCategory, CategoryServiceModel>();

            this.CreateMap<Meal, MealServiceModel>();
        }
    }
}
