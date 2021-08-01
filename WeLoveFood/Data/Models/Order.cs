using System;
using System.Collections.Generic;

namespace WeLoveFood.Data.Models
{
    public class Order
    {
        public int Id { get; init; }

        public DateTime Date { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public IEnumerable<Portion> Portions { get; init; } = new HashSet<Portion>();

        public IEnumerable<Client> Clients { get; init; } = new HashSet<Client>();
    }
}
