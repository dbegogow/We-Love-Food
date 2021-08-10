using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeLoveFood.Data.Models
{
    public class Order
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        [Required]
        public string ClientId { get; set; }

        public Client Client { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public ICollection<Portion> Portions { get; init; } = new HashSet<Portion>();
    }
}
