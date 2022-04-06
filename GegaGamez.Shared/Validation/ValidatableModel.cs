using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.Validation
{
    /// <summary>
    /// An abstract class that repsesents a model that can be validated.
    /// Uses data annotations by default.
    /// </summary>
    public abstract class ValidatableModel : IValidatableObject
    {
        /// <summary>
        /// Uses data annotations validation by default
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ICollection<ValidationResult> validationResults = new List<ValidationResult>();

            bool validated = Validator.TryValidateObject(this, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
