using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static WeLoveFood.Data.DataConstants.Restaurant;

namespace WeLoveFood.Data.Models
{
    public class Restaurant
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string CardImgUrl { get; set; }

        [Required]
        public string MainImgUrl { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? DeliveryFee { get; set; }

        public TimeSpan OpeningTime { get; set; }

        public TimeSpan ClosingTime { get; set; }

        public bool IsApproved { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public IEnumerable<MealsCategory> MealsCategories { get; init; } = new HashSet<MealsCategory>();
    }
}
