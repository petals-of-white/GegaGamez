using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GegaGamez.Shared.DomainModel
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        public byte RatingScore { get; set; }

        [Required]
        public Game Game { get; set; }

        [Required]
        public User User { get; set; } = null!;
    }
}