using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.Shared.BusinessModels
{
    public class Game : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(double.MinValue, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Range(0, 10)]
        public byte AvgRatingScore { get; set; }

        [Required]
        public Developer Developer { get; set; } = null!;

        public ICollection<Genre> Genres { get; set; }
    }
}
