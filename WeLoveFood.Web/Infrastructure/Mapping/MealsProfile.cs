using AutoMapper;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Menus;

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
