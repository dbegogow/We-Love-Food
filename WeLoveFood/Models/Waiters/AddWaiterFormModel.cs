using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Models.Constants.Users.ExceptionMessages;
using static WeLoveFood.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Models.Waiters
{
    public class AddWaiterFormModel
    {

        [Required(ErrorMessage = RequiredEmail)]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredPassword)]
        [StringLength(PasswordMaxLength, ErrorMessage = InvalidPasswordLength, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = InvalidPasswordConfirmation)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = RequiredFirstName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string FirstName { get; init; }

        [Required(ErrorMessage = RequiredLastName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        public string LastName { get; init; }

        [Required(ErrorMessage = RequiredPhoneNumber)]
        [RegularExpression(PhoneNumberRegularExpression, ErrorMessage = InvalidPhoneNumber)]
        public string PhoneNumber { get; init; }

        [Required]
        public string City { get; init; }

        [Required(ErrorMessage = RequiredAddress)]
        [StringLength(AddressMaxLength, ErrorMessage = InvalidAddressLength)]
        public string Address { get; init; }

        [Required(ErrorMessage = RequiredProfileImage)]
        public IFormFile ProfileImg { get; init; }
    }
}
