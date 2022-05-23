using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.BLL.Services;

public class GameCollectionService : IDisposable, IGameCollectionService
{
    private readonly IUnitOfWork _db;
    private readonly Expression<Func<DefaultCollection, object>> [] _dcIncludes = { dc => dc.DefaultCollectionType, dc => dc.User };
    private readonly Expression<Func<UserCollection, object>> [] _ucIncludes = { uc => uc.User };

    public GameCollectionService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="defaultCollection"></param>
    /// <param name="game"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="UniqueEntityException"></exception>
    public void AddGame(DefaultCollection defaultCollection, Game game)
    {
        var actualGame = _db.Games.Get(game.Id)
            ?? throw new EntityNotFoundException($"Game with id {game.Id} does not exist.");

        var dc = _db.DefaultCollections.Get(defaultCollection.Id)
            ?? throw new EntityNotFoundException($"Default Сollection with id {defaultCollection.Id} does not exist.");

        try
        {
            dc.Games.Add(actualGame);
            _db.Save();
        }
        catch (EntityFramework.Exceptions.Common.UniqueConstraintException ex)
        {
            var msg = $"A {typeof(Game).Name} with id {actualGame.Id} already exists in" +
                $" {typeof(DefaultCollection).Name} with id {defaultCollection.Id}.";

            throw new UniqueEntityException(msg, ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userCollection"></param>
    /// <param name="game"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="UniqueEntityException"></exception>
    public void AddGame(UserCollection userCollection, Game game)
    {
        var actualGame = _db.Games.Get(game.Id)
            ?? throw new EntityNotFoundException($"Game with id {game.Id} does not exist.");
        var uc = _db.UserCollections.Get(userCollection.Id)
            ?? throw new EntityNotFoundException($"User Сollection with id {userCollection.Id} does not exist.");

        try
        {
            uc.Games.Add(actualGame);
            _db.Save();
        }
        catch (EntityFramework.Exceptions.Common.UniqueConstraintException ex)
        {
            var msg = $"A {typeof(Game).Name} with id {actualGame.Id} already exists in" +
                $" {typeof(UserCollection).Name} with id {userCollection.Id}.";

            throw new UniqueEntityException(msg, ex);
        }
    }

    public void CreateUserCollection(UserCollection newCollection)
    {
        try
        {
            _db.UserCollections.Add(newCollection);
            _db.Save();
        }
        catch (EntityFramework.Exceptions.Common.UniqueConstraintException ex)
        {
            throw new UniqueEntityException(newCollection, ex);
        }
    }

    public void DeleteCollection(UserCollection userCollection)
    {
        try
        {
            _db.UserCollections.Remove(userCollection);
            _db.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var msg = $"Failed to remove UserCollection with id {userCollection.Id} because it doesn't exist. See the inner exception for details.";
            throw new EntityNotFoundException(msg, ex);
        }
    }

    public void Dispose() => _db.Dispose();

    public DefaultCollection? GetDefaultCollectionById(int id) =>
        _db.DefaultCollections.Get(id, _dcIncludes);

    public IEnumerable<DefaultCollectionType> GetDefaultCollectionTypes() =>
        _db.DefaultCollectionTypes.AsEnumerable();

    public IEnumerable<DefaultCollection> GetDefaultColletionsForUser(User user) =>
        _db.DefaultCollections.FindAll(dc => dc.UserId == user.Id, _dcIncludes);

    public UserCollection? GetUserCollectionById(int id) =>
        _db.UserCollections.Get(id, _ucIncludes);

    public IEnumerable<UserCollection> GetUserCollectionsForUser(User user) =>
        _db.UserCollections.FindAll(uc => uc.UserId == user.Id, _ucIncludes);

    public void RemoveGame(DefaultCollection defaultCollection, Game game)
    {
        var defaultCollectionEntity = _db.UserCollections.Get(defaultCollection.Id)
            ?? throw new EntityNotFoundException(defaultCollection, null);

        var gameEntityToRemove = _db.Games.Get(game.Id)
            ?? throw new EntityNotFoundException(game, null); ;

        defaultCollectionEntity.Games.Remove(gameEntityToRemove);

        try
        {
            _db.Update(defaultCollectionEntity);

            _db.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var msg = $"Failed to remove Game (id: {game.Id}) from Default Collection (id: {defaultCollection.Id}). " +
                $"See the inner exception for details.";

            throw new EntityNotFoundException(msg, ex);
        }
    }

    public void RemoveGame(UserCollection userCollection, Game game)
    {
        var collection = _db.UserCollections.Get(userCollection.Id)
            ?? throw new EntityNotFoundException(userCollection, null);

        var gameToRemove = _db.Games.Get(game.Id)
            ?? throw new EntityNotFoundException(game, null);

        collection.Games.Remove(gameToRemove);

        try
        {
            //_db.Update(collection);
            _db.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var msg = $"Failed to remove Game (id: {game.Id}) from User Collection (id: {userCollection.Id}). " +
                $"See the inner exception for details.";

            throw new EntityNotFoundException(msg, ex);
        }
    }
}
