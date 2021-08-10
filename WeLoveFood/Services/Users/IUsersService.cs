using WeLoveFood.Services.Models.Users;

namespace WeLoveFood.Services.Users
{
    public interface IUsersService
    {
        void ChangePersonalData(
            string userId,
            string firstName,
            string lastName,
            string phoneNumber,
            int cityId,
            string address);

        PersonalDataServiceModel PersonalData(string userId);
    }
}
