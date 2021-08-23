using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Web.Models.Constants.Users.ExceptionMessages;

namespace WeLoveFood.Web.Models.Carts
{
    public class CartUserPersonalDataFormModel
    {
        [Required(ErrorMessage = RequiredFirstName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string FirstName { get; init; }

        [Required(ErrorMessage = RequiredLastName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidLastName, MinimumLength = NameMinLength)]
        public string LastName { get; init; }

        [Required(ErrorMessage = RequiredPhoneNumber)]
        [RegularExpression(PhoneNumberRegularExpression, ErrorMessage = InvalidPhoneNumber)]
        public string PhoneNumber { get; init; }

        [Required(ErrorMessage = RequiredCity)]
        public string City { get; init; }

        [Required(ErrorMessage = RequiredAddress)]
        [StringLength(AddressMaxLength, ErrorMessage = InvalidAddressLength)]
        public string Address { get; init; }
    }
}
