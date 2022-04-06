using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.BLL.Models
{
    public class Country : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(maximumLength: 2, MinimumLength = 2)]
        [Required]
        public string TwoCharCode { get; set; } = null!;

        [StringLength(maximumLength: 3, MinimumLength = 3)]
        [Required]
        public string ThreeCharCode { get; set; } = null!;
    }
}
