using AutoMapper;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Cities;

namespace WeLoveFood.Web.Web.Infrastructure.Mapping
{
    public class CitiesProfile : Profile
    {
        public CitiesProfile()
        {
            this.CreateMap<City, CityCardServiceModel>();
        }
    }
}
