using System.ComponentModel.DataAnnotations;

using static WeLoveFood.Data.DataConstants.MealsCategory;
using static WeLoveFood.Models.Constants.Menus.ExceptionMessages;

namespace WeLoveFood.Models.Menus
{
    public class AddMealsCategoryFormModel
    {
        [Required(ErrorMessage = RequiredMealsCategoryName)]
        [StringLength(NameMaxLength, ErrorMessage = InvalidMealsCategoryName, MinimumLength = NameMinLength)]
        public string Name { get; init; }
    }
}
