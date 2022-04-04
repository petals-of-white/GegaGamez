using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Genre
    {
        private IUnitOfWork _db;

        public Genre(IUnitOfWork db)
        {
            _db = db;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; } = null!;

        [StringLength(10)]
        [Required]
        public string Description { get; set; }
    }
}
