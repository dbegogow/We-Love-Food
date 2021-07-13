using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants;

namespace WeLoveFood.Data.Models
{
    public class Restaurant
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(RestaurantNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }
    }
}
