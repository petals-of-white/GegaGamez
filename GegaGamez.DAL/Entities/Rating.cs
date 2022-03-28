using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("Rating")]
    [Index("GameId", "UserId", Name = "NIX_Rating_GameId_UserId")]
    public partial class Rating
    {
        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }
        public int UserId { get; set; }
        public byte RatingScore { get; set; }

        [ForeignKey("GameId")]
        [InverseProperty("Ratings")]
        public virtual Game Game { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("Ratings")]
        public virtual User User { get; set; } = null!;
    }
}