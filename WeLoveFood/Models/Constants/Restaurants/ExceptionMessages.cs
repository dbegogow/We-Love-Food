namespace WeLoveFood.Models.Constants.Restaurants
{
    public class ExceptionMessages
    {
        public const string InvalidName = "Името трябва да бъде между {2} и {1} символа.";
        public const string RequiredName = "Името е задължително.";

        public const string RequiredCardImg = "Снимката за картата е задължителна";
        public const string RequiredMainImg = "Основната снимка е задължителна";

        public const int MinimumDeliveryFee = 1;
        public const int MaximumDeliveryFee = 20;
        public const string InvalidDeliveryFee = "Доставката трябва да бъде между 1 лв. и 20 лв.";
    }
}
