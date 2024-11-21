using System.ComponentModel.DataAnnotations;

namespace PROG_PART_2.Models
{
    // Custom Validation Attribute to ensure that a date is greater than another specified date
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        // Property to store the name of the property that will be compared
        private readonly string _comparisonProperty;

        // Constructor to initialize the comparison property name
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        // The IsValid method performs the comparison between the current value and the specified comparison property
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the current value (the date being validated)
            var currentValue = (DateTime?)value;

            // Get the comparison property using reflection
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

            // If the comparison property is not found, throw an exception
            if (comparisonProperty == null)
                throw new ArgumentException("Property with this name not found");

            // Get the value of the comparison property (the date to compare against)
            var comparisonValue = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);

            // If either date is not set, validation passes
            if (!currentValue.HasValue || !comparisonValue.HasValue)
                return ValidationResult.Success;

            // If the current date is less than or equal to the comparison date, validation fails
            if (currentValue <= comparisonValue)
                return new ValidationResult(ErrorMessage ?? "End Date must be after Start Date.");

            // If the validation passes, return Success
            return ValidationResult.Success;
        }
    }
}
