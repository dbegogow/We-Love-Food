using Xunit;
using System.Linq;
using MyTested.AspNetCore.Mvc;
using WeLoveFood.Web.Controllers;
using WeLoveFood.Web.Models.Menus;

using static WeLoveFood.Test.Data.RestaurantsTestData;

namespace WeLoveFood.Test.Controllers
{
    public class MenusControllerTest
    {
        [Theory]
        [InlineData(1, 0, 3, "Meals category 10", 5, 1)]
        [InlineData(1, 20, 3, "Meals category 20", 6, 1)]
        [InlineData(2, 0, 4, "Meals category 20", 6, 2)]
        public void MealsShouldReturnCorrectViewWithValidData(
            int id,
            int mealsCategoryId,
            int expectedAllMealsCategoriesCount,
            string expectedMealsCategoryName,
            int expectedMealsCount,
            int expectedRestaurantId)
            => MyController<MenusController>
                .Instance(controller => controller
                    .WithData(GetRestaurantsWithMealsCategoriesAndMeals()))
                .Calling(c => c.Meals(id, mealsCategoryId))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<MenuViewModel>()
                    .Passing(model => model.AllMealsCategories.Count() == expectedAllMealsCategoriesCount &&
                                      model.MealsCategoryName == expectedMealsCategoryName &&
                                      model.Meals.Count() == expectedMealsCount &&
                                      model.Restaurant.Id == expectedRestaurantId));
    }
}
