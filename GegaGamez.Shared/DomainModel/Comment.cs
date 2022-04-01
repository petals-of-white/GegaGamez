using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 1)]
        public string Text { get; set; }

        [Required]
        public virtual Game Game { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}