namespace DiceRollingMicroservices.Common.Utils.Validations
{
    public static class ValidationConstants
    {
        public const string EMAIL_REGEX = @"^[\w.-]+@[\w.-]+\.[A-Za-z]{2,}$";

        public const int EMAIL_MIN_LENGTH = 3;

        public const int EMAIL_MAX_LENGTH = 100;

        public const int PASSWORD_MIN_LENGTH = 6;

        public const int PASSWORD_MAX_LENGTH = 100;

        public const string PASSWORD_REGEX = "^(?=.*[A-Z])(?=.*\\d).{6,}$";

        public const int FULL_NAME_MAX_LENGTH = 50;

        public const int MIN_VALUE_NUMBER = 1;

        public const int MAX_VALUE_NUMBER = 10;
    }
}
