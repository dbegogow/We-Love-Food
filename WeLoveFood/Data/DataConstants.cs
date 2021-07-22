namespace WeLoveFood.Data
{
    public class DataConstants
    {
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

            public const int DescriptionMaxLength = 70;
        }

        public class Menu
        {
            public const int CategoryMinLength = 3;
            public const int CategoryMaxLength = 25;
        }
    }
}
