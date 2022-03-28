using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("Genre")]
    [Index("Name", Name = "NIX_Genre_Name", IsUnique = true)]
    public partial class Genre
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(10)]
        public string Description { get; set; } = null!;

        [ForeignKey("GenreId")]
        [InverseProperty("Genres")]
        public virtual ICollection<Game> Games { get; set; }
    }
}