using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Common;

namespace WeLoveFood.Data.Models
{
    public class Manager
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Restaurant> Restaurants { get; init; } = new HashSet<Restaurant>();

        public ICollection<Waiter> Waiters { get; init; } = new HashSet<Waiter>();
    }
}
