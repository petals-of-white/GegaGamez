using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class GameCollectionService : IDisposable, IGameCollectionService
{
    private readonly IUnitOfWork _db;

    public GameCollectionService(IUnitOfWork db)
    {
        _db = db;
    }

    public UserCollection? GetUserCollectionById(int id)
    {
        return _db.UserCollections.Get(id);
    }

    public IEnumerable<DefaultCollectionType> GetDefaultCollectionTypes()
    {
        var dc = _db.DefaultCollectionTypes.List();
        return dc;
    }

    public void LoadCollectionGames(UserCollection userCollection)
    {
        var games = _db.Games.GetAll(
            g => g.UserCollections.Select(uc => uc.Id).Contains(userCollection.Id)
            );

        userCollection.Games = games as ICollection<Game>;
    }

    public void LoadCollectionGames(DefaultCollection defaultCollection)
    {
        var games = _db.Games.GetAll(
            g => g.DefaultCollections.Select(dc => dc.Id).Contains(defaultCollection.Id)
            );

        defaultCollection.Games = games as ICollection<Game>;
    }

    public DefaultCollection? GetDefaultCollectionById(int id) => _db.DefaultCollections.Get(id);

    public void Dispose() => _db.Dispose();
}
