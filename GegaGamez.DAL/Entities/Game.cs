using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("Game")]
    [Index("DeveloperId", Name = "NIX_Game_DeveloperId")]
    [Index("ReleaseDate", Name = "NIX_Game_ReleaseDate")]
    [Index("Title", Name = "NIX_Game_Title")]
    public partial class Game
    {
        public Game()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
            DefaultCollections = new HashSet<DefaultCollectionType>();
            Genres = new HashSet<Genre>();
            UserCollections = new HashSet<UserCollection>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = null!;

        public int DeveloperId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey("DeveloperId")]
        [InverseProperty("Games")]
        public virtual Developer Developer { get; set; } = null!;

        [InverseProperty("Game")]
        public virtual ICollection<Comment> Comments { get; set; }

        [InverseProperty("Game")]
        public virtual ICollection<Rating> Ratings { get; set; }

        [ForeignKey("GameId")]
        [InverseProperty("Games")]
        public virtual ICollection<DefaultCollectionType> DefaultCollections { get; set; }

        [ForeignKey("GameId")]
        [InverseProperty("Games")]
        public virtual ICollection<Genre> Genres { get; set; }

        [ForeignKey("GameId")]
        [InverseProperty("Games")]
        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}