namespace UserDataAccessService.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using DiceRollingMicroservices.Common.Utils.Resources;
    using DiceRollingMicroservices.Common.Utils.Validations;

    public class AuthRequest
    {
        [Required(
           ErrorMessageResourceType = typeof(ErrorMessages),
           ErrorMessageResourceName = "EmailRequired")]
        [StringLength(
           ValidationConstants.EMAIL_MAX_LENGTH,
           MinimumLength = ValidationConstants.EMAIL_MIN_LENGTH,
           ErrorMessageResourceType = typeof(ErrorMessages),
           ErrorMessageResourceName = "EmailLength")]
        [RegularExpression(
           ValidationConstants.EMAIL_REGEX,
           ErrorMessageResourceType = typeof(ErrorMessages),
           ErrorMessageResourceName = "EmailCharacters")]
        public string Email { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "PasswordRequired")]
        [StringLength(
            ValidationConstants.PASSWORD_MAX_LENGTH,
            MinimumLength = ValidationConstants.PASSWORD_MIN_LENGTH,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "PasswordLength")]
        [RegularExpression(
            ValidationConstants.PASSWORD_REGEX,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "PasswordCharacters")]
        public string Password { get; set; }
    }
}
