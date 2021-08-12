namespace WeLoveFood.Data
{
    public class DataConstants
    {
        public class Common
        {
            public const int IdMaxLength = 40;
        }

        public class City
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 20;
        }

        public class Restaurant
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
        }

        public class Meal
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 32;

            public const int WeightMinValue = 10;
            public const int WeightMaxValue = 1000;

            public const int DescriptionMaxLength = 60;
        }

        public class MealsCategory
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 25;
        }

        public class User
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int NameMinLength = 2;
            public const int NameMaxLength = 25;

            public const int AddressMaxLength = 40;

            public const string PhoneNumberRegularExpression = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
        }
    }
}
