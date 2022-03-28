using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("User")]
    [Index("Username", Name = "NIX_User_Username", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            DefaultCollections = new HashSet<DefaultCollection>();
            Ratings = new HashSet<Rating>();
            UserCollections = new HashSet<UserCollection>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; } = null!;

        [StringLength(512)]
        public string Password { get; set; } = null!;

        [StringLength(100)]
        public string? Name { get; set; }

        public int? CountryId { get; set; }

        [StringLength(500)]
        public string? About { get; set; }

        [ForeignKey("CountryId")]
        [InverseProperty("Users")]
        public virtual Country? Country { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Comment> Comments { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<DefaultCollection> DefaultCollections { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Rating> Ratings { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}