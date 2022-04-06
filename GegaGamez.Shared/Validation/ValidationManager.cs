using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Exceptions;

namespace GegaGamez.Shared.Validation
{
    public static class ValidationManager
    {
        public static void Validate(IValidatableObject objectToValidate)
        {
            ValidationContext validationContext = new(objectToValidate);

            ICollection<ValidationResult> validationErrors = (ICollection<ValidationResult>) objectToValidate.Validate(validationContext);

            if (validationErrors.Any())
            {
                throw new MultipleValidationsException("Validation was not successful", validationErrors);
            }
            else
            {
            }
        }

        public static bool TryValidate(IValidatableObject objectToValidate, ICollection<ValidationResult> validationResults)
        {
            bool result;

            ValidationContext validationContext = new(objectToValidate);

            ICollection<ValidationResult> validationErrors = (ICollection<ValidationResult>) objectToValidate.Validate(validationContext);

            if (validationErrors.Any())
            {
                result = false;

                foreach (ValidationResult validationError in validationErrors)
                {
                    validationResults.Add(validationError);
                }
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
