using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeLoveFood.Data.Models
{
    public class Client
    {
        public int Id { get; init; }

        [Required]
        public string UserId { get; init; }

        public IEnumerable<Order> Orders { get; init; } = new HashSet<Order>();

        public IEnumerable<Restaurant> Restaurants { get; init; } = new HashSet<Restaurant>();
    }
}
