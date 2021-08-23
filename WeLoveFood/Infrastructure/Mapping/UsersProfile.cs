using AutoMapper;
using WeLoveFood.Web.Data.Models;
using WeLoveFood.Web.Services.Models.Users;
using WeLoveFood.Web.Web.Models.Users;

namespace WeLoveFood.Web.Web.Infrastructure.Mapping
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            this.CreateMap<PersonalDataServiceModel, PersonalDataFormModel>();

            this.CreateMap<User, PersonalDataServiceModel>()
                .ForMember(pd => pd.City,
                    cfg =>
                        cfg.MapFrom(u => u.City.Name));
        }
    }
}
