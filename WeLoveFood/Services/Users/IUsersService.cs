using WeLoveFood.Services.Models.Users;

namespace WeLoveFood.Services.Users
{
    public interface IUsersService
    {
        PersonalDataServiceModel PersonalData(string userId);

        void ChangePersonalData(
            string userId,
            string firstName,
            string lastName,
            string phoneNumber,
            int cityId,
            string address);
    }
}
