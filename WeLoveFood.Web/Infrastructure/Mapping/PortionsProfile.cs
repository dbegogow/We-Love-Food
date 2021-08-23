using AutoMapper;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Orders;

namespace WeLoveFood.Web.Infrastructure.Mapping
{
    public class PortionsProfile : Profile
    {
        public PortionsProfile()
        {
            this.CreateMap<Portion, CartPortionServiceModel>()
                .ForMember(
                    cp => cp.Price,
                    cfg =>
                        cfg.MapFrom(p => p.Meal.Price * p.Quantity))
                .ForMember(
                    cp => cp.Meal,
                    cfg =>
                        cfg.MapFrom(p => new CartMealServiceModel
                        {
                            Id = p.Meal.Id,
                            ImgUrl = p.Meal.ImgUrl,
                            Name = p.Meal.Name,
                            Price = p.Meal.Price
                        }));
        }
    }
}
