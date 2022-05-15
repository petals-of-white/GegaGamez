using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class GameCollectionService : IDisposable, IGameCollectionService
{
    private readonly IUnitOfWork _db;

    public GameCollectionService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public UserCollection? GetUserCollectionById(int id) => _db.UserCollections.Get(id);

    public IEnumerable<DefaultCollectionType> GetDefaultCollectionTypes() => _db.DefaultCollectionTypes.AsEnumerable();

    public void LoadCollectionGames(UserCollection userCollection)
    {
        var games = _db.Games.FindAll(
            g => g.UserCollections.Select(uc => uc.Id).Contains(userCollection.Id)
            );

        userCollection.Games = games as ICollection<Game>;
    }

    public void LoadCollectionGames(DefaultCollection defaultCollection)
    {
        var games = _db.Games.FindAll(
            g => g.DefaultCollections.Select(dc => dc.Id).Contains(defaultCollection.Id)
            );

        defaultCollection.Games = games as ICollection<Game>;
    }

    public DefaultCollection? GetDefaultCollectionById(int id) => _db.DefaultCollections.Get(id);

    public void Dispose() => _db.Dispose();

    public void CreateUserCollection(UserCollection newCollection)
    {
        _db.UserCollections.Add(newCollection);
        _db.Save();
    }

    public void DeleteCollection(UserCollection userCollection)
    {
        _db.UserCollections.Remove(userCollection);
        _db.Save();
    }
}
