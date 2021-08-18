using WeLoveFood.Data.Models;
using System.Collections.Generic;
using WeLoveFood.Models.Restaurants;
using WeLoveFood.Services.Models.Restaurants;

namespace WeLoveFood.Services.Restaurants
{
    public interface IRestaurantsService
    {
        bool IsOpen(int id);

        bool IsExist(int id);

        bool AddToFavorite(int restaurantId, string userId);

        void Approve(int id);

        void Archive(int id);

        void UnArchive(int id);

        void Add(
            string userId,
            string name,
            string cardImgUrl,
            string mainImgUrl,
            decimal? deliveryFee,
            string openingTime,
            string closingTime,
            int cityId);

        void Edit(
            int id,
            string name,
            decimal? deliveryFee,
            string openingTime,
            string closingTime,
            int cityId);

        void EditCardImg(int id, string cardImgUrl);

        void EditMainImg(int id, string mainImgUrl);

        void Delete(int id);

        decimal DeliveryFee(int id);

        Restaurant Restaurant(int id);

        AllCityRestaurantsCardsQueryServiceModel AllCityRestaurantsCards(
            int cityId,
            string searchTerm,
            int currentPage,
            int carsPerPage);

        RestaurantServiceModel Information(int id);

        EditRestaurantServiceModel InformationForEdit(int id);

        IEnumerable<RestaurantCardServiceModel> Favorite(string userId);

        IEnumerable<NewRestaurantCardViewModel> NewOnes();
    }
}