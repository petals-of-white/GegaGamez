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

        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Game> Games { get; set; }

        public void GetGamesInCollection()
        {
            // get games from db
            throw new NotImplementedException();
        }
    }
}
