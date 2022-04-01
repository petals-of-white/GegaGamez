using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
    public class Game
    {
        /*
        public Game()
        {
            Comments = new HashSet<Comment>();
            Ratings = new HashSet<Rating>();
            DefaultCollections = new HashSet<DefaultCollectionType>();
            Genres = new HashSet<Genre>();
            UserCollections = new HashSet<UserCollection>();
        }
        */

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; } = null!;

        public DateTime? ReleaseDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(double.MinValue, double.MaxValue)]
        public decimal Price { get; set; }

        public Developer Developer { get; set; } = null!;
        /*
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
        */
    }
}