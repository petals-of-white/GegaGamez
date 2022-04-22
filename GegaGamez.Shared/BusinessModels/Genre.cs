using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.Shared.BusinessModels
{
    public class Genre : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        [Required]
        public string Description { get; set; }
    }
}
