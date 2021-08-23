using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;
using static WeLoveFood.Web.Models.Constants.Users.DisplayNames;
using static WeLoveFood.Web.Models.Constants.Users.ExceptionMessages;
using static WeLoveFood.Web.Areas.Identity.Pages.Account.Constants.ValidationErrorMessages;

namespace WeLoveFood.Web.Models.Waiters
{
    public class AddWaiterFormModel
    {

        [Required(ErrorMessage = RequiredEmail)]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        [Display(Name = EmailDisplay)]
        public string Email { get; set; }

        [Required(ErrorMessage = RequiredPassword)]
        [StringLength(PasswordMaxLength, ErrorMessage = InvalidPasswordLength, MinimumLength = PasswordMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = PasswordDisplay)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = InvalidPasswordConfirmation)]
        [Display(Name = ConfirmPasswordDisplay)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = RequiredFirstName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        [Display(Name = FirstNameDisplay)]
        public string FirstName { get; init; }

        [Required(ErrorMessage = RequiredLastName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidName, MinimumLength = NameMinLength)]
        [Display(Name = LastNameDisplay)]
        public string LastName { get; init; }

        [Required(ErrorMessage = RequiredPhoneNumber)]
        [RegularExpression(PhoneNumberRegularExpression, ErrorMessage = InvalidPhoneNumber)]
        [Display(Name = PhoneNumberDisplay)]
        public string PhoneNumber { get; init; }

        [Required]
        [Display(Name = CityNameDisplay)]
        public string City { get; init; }

        [Required(ErrorMessage = RequiredAddress)]
        [StringLength(AddressMaxLength, ErrorMessage = InvalidAddressLength)]
        [Display(Name = AddressDisplay)]
        public string Address { get; init; }

        [Required(ErrorMessage = RequiredProfileImage)]
        public IFormFile ProfileImg { get; init; }
    }
}
