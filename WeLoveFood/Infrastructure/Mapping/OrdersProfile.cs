using AutoMapper;
using System.Linq;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Orders;

namespace WeLoveFood.Web.Web.Infrastructure.Mapping
{
    public class OrdersProfile : Profile
    {
        private const string TimeFormat = @"hh\:mm";
        private const string DayFormat = "d";
        private const string PriceFormat = "F2";

        public OrdersProfile()
        {
            this.CreateMap<Order, ClientOrderServiceModel>()
                .ForMember(
                    co => co.Time,
                    cfg =>
                        cfg.MapFrom(o => o.Date.TimeOfDay.ToString(TimeFormat)))
                .ForMember(
                    co => co.Day,
                    cfg =>
                        cfg.MapFrom(o => o.Date.ToString(DayFormat)))
                .ForMember(
                    co => co.DeliveryFee,
                    cfg =>
                        cfg.MapFrom(o => o.Restaurant.DeliveryFee.ToString()))
                .ForMember(
                    co => co.TotalPrice,
                    cfg =>
                        cfg.MapFrom(o => o.TotalPrice.ToString(PriceFormat)))
                .ForMember(
                    co => co.Portions,
                    cfg =>
                        cfg.MapFrom(o => o.Portions.Select(p => new PortionOrderServiceModel
                        {
                            MealName = p.Meal.Name,
                            Quantity = p.Quantity,
                            Price = p.Meal.Price
                        })));

            this.CreateMap<Order, RestaurantOrderServiceModel>()
                .ForMember(
                    co => co.Time,
                    cfg =>
                        cfg.MapFrom(o => o.Date.TimeOfDay.ToString(TimeFormat)))
                .ForMember(
                    co => co.Day,
                    cfg =>
                        cfg.MapFrom(o => o.Date.ToString(DayFormat)))
                .ForMember(
                    co => co.TotalPrice,
                    cfg =>
                        cfg.MapFrom(o => o.TotalPrice.ToString(PriceFormat)))
                .ForMember(
                    co => co.Portions,
                    cfg =>
                        cfg.MapFrom(o => o.Portions.Select(p => new PortionOrderServiceModel
                        {
                            MealName = p.Meal.Name,
                            Quantity = p.Quantity,
                            Price = p.Meal.Price
                        })));
        }
    }
}
