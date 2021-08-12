using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.MealsCategory;

namespace WeLoveFood.Data.Models
{
    public class MealsCategory
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public ICollection<Meal> Meals { get; init; } = new HashSet<Meal>();
    }
}
