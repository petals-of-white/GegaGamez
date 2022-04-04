using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Rating
    {
        private IUnitOfWork _db;

        public Rating(IUnitOfWork db)
        {
            _db = db;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        public byte RatingScore { get; set; }

        [Required]
        public Game Game { get; set; }
    }
}
