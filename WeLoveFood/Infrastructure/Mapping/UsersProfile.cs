using AutoMapper;
using WeLoveFood.Data.Models;
using WeLoveFood.Models.Users;
using WeLoveFood.Services.Models.Users;

namespace WeLoveFood.Infrastructure.Mapping
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
