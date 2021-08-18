namespace WeLoveFood.Models.Constants.Users
{
    public class ExceptionMessages
    {
        public const string InvalidName = "Името трябва да бъде между {2} и {1} символа.";
        public const string InvalidLastName = "Фамилията трябва да бъде между {2} и {1} символа.";
        public const string RequiredFirstName = "Името е задължително.";
        public const string RequiredLastName = "Фамилията е задължителна.";

        public const string InvalidPhoneNumber = "Невалиден телефонен номер.";
        public const string RequiredPhoneNumber = "Телефонният номер е задължителен.";

        public const string RequiredCity = "Градът е задължителен.";

        public const string InvalidAddressLength = "Адресът трябва да бъде от максимум {1} символа.";
        public const string RequiredAddress = "Адресът е задължителен.";

        public const string RequiredProfileImage = "Профилната снимка е задължителна";
    }
}
