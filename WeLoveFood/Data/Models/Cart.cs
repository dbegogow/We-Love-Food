using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Common;

namespace WeLoveFood.Data.Models
{
    public class Cart
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string ClientId { get; set; }

        public IEnumerable<Portion> Portions { get; init; } = new HashSet<Portion>();
    }
}
