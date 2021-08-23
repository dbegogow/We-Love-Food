namespace WeLoveFood.Web.Data.Models
{
    public class Portion
    {
        public int Id { get; init; }

        public int Quantity { get; set; }

        public int MealId { get; set; }

        public Meal Meal { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }

        public string CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
