using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class DefaultCollection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DefaultCollectionType DefaultCollectionType { get; set; }

        [Required]
        public User User { get; set; }
    }
}