namespace WeLoveFood.Web.Models.Constants.Cities
{
    public class ExceptionMessages
    {
        public const string InvalidCity = "Няма ресторанти от този град.";

        public const string RestaurantNotFromCity = "Ресторантът не се намира в този град.";

        public const string RequiredCityName = "Името на града е задължително.";
        public const string RequiredCityImg = "Снимката на града е задължителна.";

        public const string InvalidCityName = "Името на града трябва да бъде между {2} и {1} символа.";
    }
}
