using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.Shared.BusinessModels
{
    public class DefaultCollectionType : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
