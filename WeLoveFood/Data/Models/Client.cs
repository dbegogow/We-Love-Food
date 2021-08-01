using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.User;

namespace WeLoveFood.Data.Models
{
    public class Client
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<Order> Orders { get; init; } = new HashSet<Order>();

        public IEnumerable<Restaurant> Restaurants { get; init; } = new HashSet<Restaurant>();
    }
}
