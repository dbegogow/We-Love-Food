using WeLoveFood.Web.Services.Models.Users;

namespace WeLoveFood.Web.Services.Users
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

        void UpdateProfileImage(string id, string profileImgUrl);

        PersonalDataServiceModel PersonalData(string id);
    }
}
