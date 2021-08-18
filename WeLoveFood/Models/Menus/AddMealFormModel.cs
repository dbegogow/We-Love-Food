using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Meal;
using static WeLoveFood.Models.Constants.Menus.ExceptionMessages;

namespace WeLoveFood.Models.Menus
{
    public class AddMealFormModel
    {
        [Required(ErrorMessage = RequiredMealName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidMealName, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredWeight)]
        [Range(WeightMinValue, WeightMaxValue, ErrorMessage = InvalidMealWeight)]
        public int Weight { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = InvalidMealsCategoryName)]
        public string Description { get; set; }

        [Required(ErrorMessage = RequiredMealImg)]
        public IFormFile Img { get; set; }

        [Required(ErrorMessage = RequiredMealPrice)]
        public decimal Price { get; set; }

        public string MealsCategory { get; init; }
    }
}
