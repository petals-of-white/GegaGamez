using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.BLL.Models
{
    public class UserCollection : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        //[Required]
        //public User User { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
