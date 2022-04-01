using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class UserCollection
    {
        /*
        public UserCollection()
        {
            Games = new HashSet<Game>();
        }
        */

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        public User User { get; set; }

        /*
        [ForeignKey("UserCollectionId")]
        [InverseProperty("UserCollections")]
        public virtual ICollection<Game> Games { get; set; }
        */
    }
}