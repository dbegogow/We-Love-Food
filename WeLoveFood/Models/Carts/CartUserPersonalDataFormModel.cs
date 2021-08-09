using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Models.Constants.Users.ExceptionMessages;

namespace WeLoveFood.Models.Carts
{
    public class CartUserPersonalDataFormModel
    {
        [Required]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string FirstName { get; init; }

        [Required]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string LastName { get; init; }

        [Required]
        [RegularExpression(PhoneNumberRegularExpression, ErrorMessage = InvalidPhoneNumber)]
        public string PhoneNumber { get; init; }

        [Required]
        public string City { get; init; }
        
        [Required]
        [StringLength(AddressMaxLength, ErrorMessage = InvalidAddressLength)]
        public string Address { get; init; }
    }
}
