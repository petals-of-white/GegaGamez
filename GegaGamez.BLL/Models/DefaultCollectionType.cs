using System.ComponentModel.DataAnnotations;

namespace GegaGamez.BLL.Models
{
    public class DefaultCollectionType : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        //public ICollection<DefaultCollection> DefaultCollections { get; set; }

        //public ICollection<Game> Games { get; set; }
    }
}
