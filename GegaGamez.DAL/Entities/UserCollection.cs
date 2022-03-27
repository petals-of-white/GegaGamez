using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("UserCollection")]
    [Index("UserId", "Name", Name = "NIX_UserCollection_UserId_Name", IsUnique = true)]
    public partial class UserCollection
    {
        public UserCollection()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string? Description { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("UserCollections")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("UserCollectionId")]
        [InverseProperty("UserCollections")]
        public virtual ICollection<Game> Games { get; set; }
    }
}
