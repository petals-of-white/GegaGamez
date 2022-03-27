using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("DefaultCollectionType")]
    [Index("Name", Name = "NIX_DefaultCollectionType_Name", IsUnique = true)]
    public partial class DefaultCollectionType
    {
        public DefaultCollectionType()
        {
            DefaultCollections = new HashSet<DefaultCollection>();
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [InverseProperty("DefaultCollectionType")]
        public virtual ICollection<DefaultCollection> DefaultCollections { get; set; }

        [ForeignKey("DefaultCollectionId")]
        [InverseProperty("DefaultCollections")]
        public virtual ICollection<Game> Games { get; set; }
    }
}
