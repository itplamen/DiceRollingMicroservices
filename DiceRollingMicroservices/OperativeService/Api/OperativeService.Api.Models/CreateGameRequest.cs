namespace OperativeService.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using DiceRollingMicroservices.Common.Utils.Attributes;
    using DiceRollingMicroservices.Common.Utils.Resources;
    using DiceRollingMicroservices.Common.Utils.Validations;

    public class CreateGameRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DieTypeValidation]
        public string DieType { get; set; }

        [Required]
        [Range(ValidationConstants.MIN_VALUE_NUMBER, 
            ValidationConstants.MAX_VALUE_NUMBER,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "MaxRange")]
        public int MaxUsers { get; set; }

        [Required]
        [Range(ValidationConstants.MIN_VALUE_NUMBER,
            ValidationConstants.MAX_VALUE_NUMBER,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "MaxRange")]
        public int MaxRounds { get; set; }

        [Required]
        [Range(ValidationConstants.MIN_VALUE_NUMBER,
            ValidationConstants.MAX_VALUE_NUMBER,
            ErrorMessageResourceType = typeof(ErrorMessages),
            ErrorMessageResourceName = "MaxRange")]
        public int DicePerUser { get; set; }
    }
}
