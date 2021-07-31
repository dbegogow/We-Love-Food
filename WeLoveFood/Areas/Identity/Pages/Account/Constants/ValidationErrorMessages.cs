namespace WeLoveFood.Areas.Identity.Pages.Account.Constants
{
    public static class ValidationErrorMessages
    {
        public const string RequiredEmail = "Валидният имейл е задължителен.";
        public const string InvalidEmail = "Невалиден емейл.";
        public const string RequiredPassword = "Паролата е задължителна.";
        public const string InvalidPasswordContent = "Паролата трябва да съдържа главна буква, малка буква и цифра.";
        public const string InvalidPasswordLength = "Паролата трябва да бъде между {2} и {1} символа.";
        public const string InvalidPasswordConfirmation = "Паролата и потвърждението на паролата не съвпадат.";

        public const string InvalidAttempt = "Невалиден опит за влизане.";

        public const string InvalidPassword = "Грешна парола.";
        public const string RequiredField = "Полето е задължително.";

        public const string AlreadyExistUserWithEmail = "Вече съществува потребител с този имейл.";
        public const string InvalidChangeEmail = "Грешка в смяната на имейла.";
    }
}
