using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants;

namespace WeLoveFood.Data.Models
{
    public class City
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }
    }
}
