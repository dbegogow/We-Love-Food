using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Web.Data.DataConstants.User;
using static WeLoveFood.Web.Models.Constants.Users.ExceptionMessages;

namespace WeLoveFood.Web.Models.Users
{
    public class PersonalDataFormModel
    {
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string FirstName { get; init; }

        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string LastName { get; init; }

        [RegularExpression(PhoneNumberRegularExpression, ErrorMessage = InvalidPhoneNumber)]
        public string PhoneNumber { get; init; }

        public string City { get; init; }

        [StringLength(AddressMaxLength, ErrorMessage = InvalidAddressLength)]
        public string Address { get; init; }

        public IFormFile ProfileImg { get; init; }

        public string ProfileImgUrl { get; init; }
    }
}
