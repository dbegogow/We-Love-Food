using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Models.Constants.Users.ExceptionMessages;

namespace WeLoveFood.Models.Users
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
    }
}
