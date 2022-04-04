using System.ComponentModel.DataAnnotations;

namespace GegaGamez.Shared.DomainModel
{
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

        public ICollection<Comment> Comments { get; set; }

        public ICollection<DefaultCollection> DefaultCollections { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}
