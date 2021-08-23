namespace WeLoveFood.Web.Services.Models.Restaurants
{
    public class ManagersRestaurantServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string CardImgUrl { get; init; }

        public bool IsApproved { get; init; }

        public bool IsArchived { get; init; }
    }
}
