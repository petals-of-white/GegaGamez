using System.ComponentModel.DataAnnotations;

namespace GegaGamez.BLL.Models
{
    public class Genre : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(10)]
        [Required]
        public string Description { get; set; }

        //public ICollection<Game> Games { get; set; }
    }
}
