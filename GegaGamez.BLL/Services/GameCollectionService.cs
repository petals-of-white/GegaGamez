using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;
using GegaGamez.Shared.BusinessModels;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Validation;

namespace GegaGamez.BLL.Services
{
    public class GameCollectionService : IDisposable
    {
        private readonly IUnitOfWork _db;

        public GameCollectionService(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public void LoadCollectionGames(UserCollection userCollection)
        {
            var games = _db.Games.GetAll(
                g => g.UserCollections.Select(uc => uc.Id).Contains(userCollection.Id)
                );

            userCollection.Games = AutoMapping.Mapper.Map<IEnumerable<Game>>(games).ToList();
        }

        public void LoadCollectionGames(DefaultCollection defaultCollection)
        {
            var games = _db.Games.GetAll(
                g => g.DefaultCollections.Select(dc => dc.Id).Contains(defaultCollection.Id)
                );

            defaultCollection.Games = AutoMapping.Mapper.Map<IEnumerable<Game>>(games).ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
