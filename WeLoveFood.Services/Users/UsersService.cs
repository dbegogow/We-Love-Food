using AutoMapper;
using System.Linq;
using AutoMapper.QueryableExtensions;
using WeLoveFood.Web.Data;
using WeLoveFood.Web.Services.Models.Users;

namespace WeLoveFood.Web.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly WeLoveFoodDbContext _data;
        private readonly IConfigurationProvider _mapper;

        public UsersService(
            WeLoveFoodDbContext data,
            IMapper mapper)
        {
            _data = data;
            _mapper = mapper.ConfigurationProvider;
        }

        public void ChangePersonalData(
            string userId,
            string firstName,
            string lastName,
            string phoneNumber,
            int cityId,
            string address)
        {
            var user = this._data
                .Users
                .Find(userId);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.CityId = cityId;
            user.Address = address;

            this._data.SaveChanges();
        }

        public void UpdateProfileImage(string id, string profileImgUrl)
        {
            this._data
                .Users
                .Find(id)
                .ProfileImgUrl = profileImgUrl;

            this._data.SaveChanges();
        }

        public PersonalDataServiceModel PersonalData(string id)
            => this._data
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<PersonalDataServiceModel>(this._mapper)
                .FirstOrDefault();
    }
}
