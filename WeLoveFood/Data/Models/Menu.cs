using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeLoveFood.Data.Models
{
    public class Menu
    {
        public int Id { get; init; }

        [Required]
        public string Category { get; set; }

        public IEnumerable<Meal> Meals { get; init; } = new HashSet<Meal>();
    }
}
