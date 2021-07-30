using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;

namespace WeLoveFood.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public int? CityId { get; set; }

        public City City { get; set; }
    }
}
