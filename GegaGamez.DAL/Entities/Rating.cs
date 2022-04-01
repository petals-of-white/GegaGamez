using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("Rating")]
    [Index("GameId", Name = "NIX_Rating_GameId")]
    [Index("RatingScore", Name = "NIX_Rating_RatingScore")]
    public partial class Rating
    {
        [Key]
        public int UserId { get; set; }

        [Key]
        public int GameId { get; set; }

        [Key]
        public byte RatingScore { get; set; }

        [ForeignKey("GameId")]
        [InverseProperty("Ratings")]
        public virtual Game Game { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("Ratings")]
        public virtual User User { get; set; } = null!;
    }
}
