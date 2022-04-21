using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.Shared.BusinessModels
{
    public class User : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 6)]
        [Required]
        public string Username { get; set; }

        [StringLength(512, MinimumLength = 6)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? About { get; set; }

        public Country? Country { get; set; }

        public ICollection<DefaultCollection> DefaultCollections { get; set; }

        public virtual ICollection<UserCollection> UserCollections { get; set; }
    }
}
