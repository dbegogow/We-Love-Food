using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Category;

namespace WeLoveFood.Data.Models
{
    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public IEnumerable<Meal> Meals { get; init; } = new HashSet<Meal>();
    }
}
