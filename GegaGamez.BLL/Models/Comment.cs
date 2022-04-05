using System.ComponentModel.DataAnnotations;

namespace GegaGamez.BLL.Models
{
    public class Comment : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 1000, MinimumLength = 1)]
        public string Text { get; set; }

        [Required]
        public Game Game { get; set; }

        [Required]
        public User User { get; set; }
    }
}
