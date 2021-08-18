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

        public const string RequiredOpeningTime = "Времето на отваряне е задължително";
        public const string RequiredClosingTime = "Времето на затваряне е задължително";

        public const string InvalidWorkingTime = "Времето на работа е невалидно. Трябва да бъде в формат часове/минути - '09:30'";
    }
}
