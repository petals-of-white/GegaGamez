using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class DefaultCollection
    {
        private IUnitOfWork _db;

        public DefaultCollection(IUnitOfWork db)
        {
            _db = db;
        }

        public int Id { get; set; }

        public DefaultCollectionType DefaultCollectionType { get; set; }

        public ICollection<Game> Games { get; set; }

        public void GetGamesInCollection()
        {
            // get games from db
            throw new NotImplementedException();
        }
    }
}
