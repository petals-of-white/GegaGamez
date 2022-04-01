using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GegaGamez.Shared.DomainModel
{
    [Table("User")]
    public class User
    {
        /*
        public User()
        {
            Comments = new HashSet<Comment>();
            DefaultCollections = new HashSet<DefaultCollection>();
            Ratings = new HashSet<Rating>();
            UserCollections = new HashSet<UserCollection>();
        }
        */

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Username { get; set; }

        [StringLength(512)]
        [Required]
        public string Password { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? About { get; set; }

        public Country? Country { get; set; }

        /*

        [InverseProperty("User")]
        public virtual ICollection<Comment> Comments { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<DefaultCollection> DefaultCollections { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Rating> Ratings { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserCollection> UserCollections { get; set; }
        */
    }
}