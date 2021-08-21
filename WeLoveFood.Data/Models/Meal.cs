using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static WeLoveFood.Data.DataConstants.Meal;

namespace WeLoveFood.Data.Models
{
    public class Meal
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int Weight { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string ImgUrl { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; }

        public int MealsCategoryId { get; set; }

        public MealsCategory MealsCategory { get; set; }

        public ICollection<Portion> Portions { get; init; } = new HashSet<Portion>();
    }
}
