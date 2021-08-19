using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.MealsCategory;
using static WeLoveFood.Models.Constants.Menus.DisplayNames;
using static WeLoveFood.Models.Constants.Menus.ExceptionMessages;

namespace WeLoveFood.Models.Menus
{
    public class MealsCategoryFormModel
    {
        [Required(ErrorMessage = RequiredMealsCategoryName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidMealsCategoryName, MinimumLength = NameMinLength)]
        [Display(Name = MealsCategoryNameDisplay)]
        public string Name { get; init; }
    }
}
