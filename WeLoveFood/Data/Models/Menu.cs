using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Menu;

namespace WeLoveFood.Data.Models
{
    public class Menu
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryMaxLength)]
        public string Category { get; set; }

        public IEnumerable<Meal> Meals { get; init; } = new HashSet<Meal>();
    }
}
