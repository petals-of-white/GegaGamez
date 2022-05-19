using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class GameCollectionService : IDisposable, IGameCollectionService
{
    private readonly IUnitOfWork _db;

    public GameCollectionService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void AddGame(DefaultCollection defaultCollection, Game game)
    {
        var actualGame = _db.Games.Get(game.Id);
        var dc = _db.DefaultCollections.Get(defaultCollection.Id) ?? throw new ArgumentException("Entry was not found");
        dc.Games.Add(actualGame);
        _db.Save();
    }

    public void AddGame(UserCollection userCollection, Game game)
    {
        var actualGame = _db.Games.Get(game.Id);
        var uc = _db.UserCollections.Get(userCollection.Id) ?? throw new ArgumentException("Entry was not found");
        uc.Games.Add(actualGame);
        _db.Save();
    }

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

    public void Dispose() => _db.Dispose();

    public DefaultCollection? GetDefaultCollectionById(int id) => _db.DefaultCollections.Get(id);

    public IEnumerable<DefaultCollectionType> GetDefaultCollectionTypes() => _db.DefaultCollectionTypes.AsEnumerable();

    public IEnumerable<DefaultCollection> GetDefaultColletionsForUser(User user) =>
        _db.DefaultCollections.FindAll(dc => dc.UserId == user.Id);

    public UserCollection? GetUserCollectionById(int id) => _db.UserCollections.Get(id);

    public IEnumerable<UserCollection> GetUserCollectionsForUser(User user) =>
        _db.UserCollections.FindAll(uc => uc.UserId == user.Id);

    public void RemoveGame(DefaultCollection defaultCollection, Game game)
    {
        var defaultCollectionEntity = _db.UserCollections.Get(defaultCollection.Id);

        var gameEntityToRemove = _db.Games.Get(game.Id);

        if (defaultCollectionEntity is null || gameEntityToRemove is null)
        {
            throw new ArgumentException("A collection or game doesn't exist!");
        }

        defaultCollectionEntity.Games.Remove(gameEntityToRemove);

        _db.Update(defaultCollectionEntity);

        _db.Save();
    }

    public void RemoveGame(UserCollection userCollection, Game game)
    {
        var collection = _db.UserCollections.Get(userCollection.Id);

        var gameToRemove = _db.Games.Get(game.Id);

        if (collection is null || gameToRemove is null)
        {
            throw new ArgumentException("A collection or game doesn't exist!");
        }

        collection.Games.Remove(gameToRemove);

        _db.Update(collection);

        _db.Save();
    }
}
