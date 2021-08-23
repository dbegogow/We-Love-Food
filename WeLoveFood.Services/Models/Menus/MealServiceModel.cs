namespace WeLoveFood.Web.Services.Models.Menus
{
    public class MealServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Weight { get; init; }

        public string Description { get; init; }

        public string ImgUrl { get; init; }

        public decimal Price { get; init; }
    }
}
