using AutoMapper;
using WeLoveFood.Data.Models;
using WeLoveFood.Services.Models.Cities;

namespace WeLoveFood.Web.Infrastructure.Mapping
{
    public class CitiesProfile : Profile
    {
        public CitiesProfile()
        {
            this.CreateMap<City, CityCardServiceModel>();
        }
    }
}
