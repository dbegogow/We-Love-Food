﻿using AutoMapper;
using System.Linq;
using WeLoveFood.Data.Models;
using WeLoveFood.Models.Restaurants;

namespace WeLoveFood.Infrastructure.Mapping
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile()
        {
            this.CreateMap<Restaurant, NewRestaurantCardViewModel>()
                .ForMember(nrc => nrc.MealsCategories,
                    cfg =>
                        cfg.MapFrom(r => r.MealsCategories.Select(mc => mc.Name)));
        }
    }
}