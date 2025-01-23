using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.Validations;

public class HalfStepRangeAttribute : ValidationAttribute
{
    private readonly double _minimum;
    private readonly double _maximum;

    public HalfStepRangeAttribute(double minimum, double maximum)
    {
        this._minimum = minimum;
        this._maximum = maximum;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success; // Allow null values (use [Required] for mandatory validation)
        }

        if (value is double number)
        {
            // Check range
            if (number < _minimum || number > _maximum)
            {
                return new ValidationResult(
                    $"The value must be between {_minimum} and {_maximum}.",
                    new[] { validationContext.MemberName }
                );
            }

            // Check for 0.5 increments
            if ((number * 10) % 5 != 0)
            {
                return new ValidationResult(
                    "The value must be in 0.5 increments.",
                    new[] { validationContext.MemberName }
                );
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid data type.", new[] { validationContext.MemberName });
    }
}
