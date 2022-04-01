using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class DefaultCollectionType
    {
        /*
        public DefaultCollectionType()
        {
            DefaultCollections = new HashSet<DefaultCollection>();
            Games = new HashSet<Game>();
        }
        */

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        /*
        [InverseProperty("DefaultCollectionType")]
        public virtual ICollection<DefaultCollection> DefaultCollections { get; set; }

        [ForeignKey("DefaultCollectionId")]
        [InverseProperty("DefaultCollections")]
        public virtual ICollection<Game> Games { get; set; }
        */
    }
}