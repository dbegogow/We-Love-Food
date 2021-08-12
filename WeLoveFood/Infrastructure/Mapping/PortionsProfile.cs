using AutoMapper;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Orders;

namespace WeLoveFood.Infrastructure.Mapping
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
