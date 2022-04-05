using System.ComponentModel.DataAnnotations;

namespace GegaGamez.BLL.Models
{
    public class DefaultCollection : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DefaultCollectionType DefaultCollectionType { get; set; }

        public ICollection<Game> Games { get; set; }

        //[Required]
        //public User User { get; set; }
    }
}
