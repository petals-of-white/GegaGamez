using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class Country
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
