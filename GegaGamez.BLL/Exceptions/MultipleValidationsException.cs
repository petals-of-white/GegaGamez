using System.ComponentModel.DataAnnotations;

namespace GegaGamez.BLL.Exceptions
{
    public class MultipleValidationsException : Exception
    {
        public MultipleValidationsException(string message, ICollection<ValidationResult> validationResults) : base(message)
        {
            ValidationResults = validationResults;
        }

        public ICollection<ValidationResult> ValidationResults { get; set; }
    }
}
