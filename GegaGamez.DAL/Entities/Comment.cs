using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("Comment")]
    [Index("GameId", Name = "NIX_Comment_GameId")]
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }

        public int GameId { get; set; }
        public int UserId { get; set; }

        [StringLength(1000)]
        public string Text { get; set; } = null!;

        [ForeignKey("GameId")]
        [InverseProperty("Comments")]
        public virtual Game Game { get; set; } = null!;

        [ForeignKey("UserId")]
        [InverseProperty("Comments")]
        public virtual User User { get; set; } = null!;
    }
}