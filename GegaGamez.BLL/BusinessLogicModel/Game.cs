using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Game
    {
        private IUnitOfWork _db;

        public Game(IUnitOfWork db)
        {
            _db = db;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public byte AvgRatingScore { get; set; }

        public Developer Developer { get; set; }

        public ICollection<Rating> Ratings { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public void GetComments()
        {
            // geet Comments from db
            throw new NotImplementedException();
        }

        public void GetGenres()
        {
            // get Genres from db
            throw new NotImplementedException();
        }

        public void GetAvgRatingScore()
        {
            // get avg rating for a game
            throw new NotImplementedException();
        }
    }
}
