using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Common;

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

        public string CartId { get; set; }

        public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; init; } = new HashSet<Order>();

        public ICollection<Restaurant> Restaurants { get; init; } = new HashSet<Restaurant>();
    }
}
