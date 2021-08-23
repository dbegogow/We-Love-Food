using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeLoveFood.Data.Models
{
    public class Order
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        public bool IsAccepted { get; set; }

        public string Address { get; init; }

        [Required]
        public string ClientId { get; set; }

        public Client Client { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        public ICollection<Portion> Portions { get; init; } = new HashSet<Portion>();
    }
}
