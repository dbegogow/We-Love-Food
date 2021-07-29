using WeLoveFood.Data;
using System.ComponentModel.DataAnnotations;

namespace WeLoveFood.Models.Users
{
    public class ChangePersonalDataFormModel
    {
        [StringLength(DataConstants.User.NameMaxLength, MinimumLength = DataConstants.User.NameMinLength)]
        public string FirstName { get; init; }

        [StringLength(DataConstants.User.NameMaxLength, MinimumLength = DataConstants.User.NameMinLength)]
        public string LastName { get; init; }

        [RegularExpression(DataConstants.User.PhoneNumberRegularExpression)]
        public string PhoneNumber { get; init; }

        [StringLength(DataConstants.City.NameMaxLength)]
        public string City { get; init; }

        [StringLength(DataConstants.User.AddressMaxLength)]
        public string Address { get; init; }
    }
}
