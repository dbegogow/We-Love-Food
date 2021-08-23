﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Web.Data.DataConstants.City;

namespace WeLoveFood.Web.Data.Models
{
    public class City
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; } = new HashSet<Restaurant>();
    }
}
