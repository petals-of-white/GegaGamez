using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class UserCollection
    {
        private IUnitOfWork _db;

        public UserCollection(IUnitOfWork db)
        {
            _db = db;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        public ICollection<Game> Games { get; set; }

        public void GetGamesInCollection()
        {
            // get games from db
            throw new NotImplementedException();
        }
    }
}
