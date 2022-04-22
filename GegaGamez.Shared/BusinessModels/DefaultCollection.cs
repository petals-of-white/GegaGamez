using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.Shared.BusinessModels
{
    public class DefaultCollection : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DefaultCollectionType DefaultCollectionType { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
