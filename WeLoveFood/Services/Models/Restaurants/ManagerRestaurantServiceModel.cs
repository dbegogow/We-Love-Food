namespace WeLoveFood.Services.Models.Restaurants
{
    public class ManagerRestaurantServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public bool IsApproved { get; init; }

        public bool IsArchived { get; init; }
    }
}
