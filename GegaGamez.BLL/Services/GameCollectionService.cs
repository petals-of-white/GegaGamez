using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;

namespace GegaGamez.BLL.Services;

public class GameCollectionService : IDisposable
{
    private readonly IUnitOfWork _db;

    public GameCollectionService(IUnitOfWork db)
    {
        _db = db;
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
