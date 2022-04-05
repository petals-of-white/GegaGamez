using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("DefaultCollection")]
    [Index("UserId", "DefaultCollectionTypeId", Name = "NIX_DefaultCollection_UserId_DefaultCollectionTypeId", IsUnique = true)]
    public partial class DefaultCollection
    {
        public DefaultCollection()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int DefaultCollectionTypeId { get; set; }

        [ForeignKey("DefaultCollectionTypeId")]
        [InverseProperty("DefaultCollections")]
        public virtual DefaultCollectionType DefaultCollectionType { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("DefaultCollections")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("DefaultCollectionId")]
        [InverseProperty("DefaultCollections")]
        public virtual ICollection<Game> Games { get; set; }
    }
}
