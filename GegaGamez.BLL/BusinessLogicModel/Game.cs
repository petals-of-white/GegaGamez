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

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public byte AvgRatingScore { get; set; }

        public Developer Developer { get; set; }

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
