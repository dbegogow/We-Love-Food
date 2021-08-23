using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Web.Data.DataConstants.User;

namespace WeLoveFood.Web.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        public string ProfileImgUrl { get; set; }

        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        public int? CityId { get; set; }

        public City City { get; set; }
    }
}
