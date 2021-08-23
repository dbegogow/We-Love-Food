using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.Meal;
using static WeLoveFood.Web.Models.Constants.Menus.DisplayNames;
using static WeLoveFood.Web.Models.Constants.Menus.ExceptionMessages;

namespace WeLoveFood.Web.Models.Menus
{
    public class AddMealFormModel
    {
        [Required(ErrorMessage = RequiredMealName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidMealName, MinimumLength = NameMinLength)]
        [Display(Name = MealNameDisplay)]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredWeight)]
        [Range(WeightMinValue, WeightMaxValue, ErrorMessage = InvalidMealWeight)]
        [Display(Name = MealWeightDisplay)]
        public int Weight { get; set; }

        [StringLength(DescriptionMaxLength, ErrorMessage = InvalidMealsCategoryName)]
        [Display(Name = MealDescriptionDisplay)]
        public string Description { get; set; }

        [Required(ErrorMessage = RequiredMealImg)]
        public IFormFile Img { get; set; }

        [Required(ErrorMessage = RequiredMealPrice)]
        [Display(Name = MealPriceDisplay)]
        public decimal Price { get; set; }

        [Display(Name = MealCategoryDisplay)]
        public string MealsCategory { get; init; }
    }
}
