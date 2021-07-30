using System.Linq;
using WeLoveFood.Data;
using WeLoveFood.Services.Models.Users;

namespace WeLoveFood.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly WeLoveFoodDbContext _data;

        public UsersService(WeLoveFoodDbContext data)
            => _data = data;

        public PersonalDataServiceModel PersonalData(string userId)
            => this._data
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new PersonalDataServiceModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    City = u.City.Name,
                    Address = u.Address
                })
                .FirstOrDefault();

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
    }
}
