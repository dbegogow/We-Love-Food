using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.MealsCategory;
using static WeLoveFood.Web.Models.Constants.Menus.DisplayNames;
using static WeLoveFood.Web.Models.Constants.Menus.ExceptionMessages;

namespace WeLoveFood.Web.Models.Menus
{
    public class MealsCategoryFormModel
    {
        [Required(ErrorMessage = RequiredMealsCategoryName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidMealsCategoryName, MinimumLength = NameMinLength)]
        [Display(Name = MealsCategoryNameDisplay)]
        public string Name { get; init; }
    }
}
