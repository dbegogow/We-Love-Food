using System;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Web.Data.DataConstants.Common;

namespace WeLoveFood.Web.Data.Models
{
    public class Waiter
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public string ManagerId { get; set; }

        public Manager Manager { get; set; }

        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
