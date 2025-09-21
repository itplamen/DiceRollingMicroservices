namespace DiceRollingMicroservices.Common.Utils.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using OperativeService.Data.Models;

    public class DieTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string str && Enum.GetNames(typeof(DieType)).Contains(str))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid DieType. Allowed values are: {string.Join(", ", Enum.GetNames(typeof(DieType)))}");
        }
    }
}
