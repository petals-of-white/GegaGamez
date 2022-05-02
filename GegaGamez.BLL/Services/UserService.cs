using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class UserService : IDisposable, IUserService
{
    private readonly IUnitOfWork _db;

    public UserService(IUnitOfWork db)
    {
        _db = db;
    }

    public User? GetById(int id)
    {
        //var userEntity = _db.Users.Get(id);
        var user = _db.Users.Get(id);
        //var user = AutoMapping.Mapper.Map<User>(userEntity);

        return user;
    }

    public IEnumerable<User> GetAll()
    {
        var allUsers = _db.Users.List();
        //return AutoMapping.Mapper.Map<IEnumerable<User>>(allUsers);
        return allUsers;
    }

    public IEnumerable<User> FindByUsername(string username)
    {
        var usersByUsername = _db.Users.GetAllByUsername(username);

        //var usersByUsername = AutoMapping.Mapper.Map<IEnumerable<User>>(usersEntitiesByUsername);

        return usersByUsername;
    }

    public void LoadUsersCollections(User user)
    {
        user.DefaultCollections = _db.DefaultCollections.GetByUser(user).ToList();

        user.UserCollections = _db.UserCollections.GetAllByUser(user).ToList();
    }

    public void AddComment(Comment comment)
    {
        _db.Comments.Add(comment);
        _db.Save();
    }

    /// <summary>
    /// </summary>
    /// <param name="rating"></param>
    /// <exception cref="MultipleValidationsException"></exception>
    public void RateGame(Rating rating)
    {
        //try
        //{
        //    ValidationManager.Validate(rating);
        //}
        //catch (MultipleValidationsException)
        //{
        //    throw;
        //}

        //var ratingEntity = AutoMapping.Mapper.Map<Shared.Entities.Rating>(rating);

        // check if user rating for a game already exists
        bool ratingExist = _db.Ratings.GetAll(
            r => r.UserId == rating.UserId
            && r.GameId == rating.GameId)
            .Count() == 1;

        if (ratingExist)
        {
            _db.Update(rating);
        }
        else
        {
            _db.Ratings.Add(rating);
        }

        _db.Save();
    }

    public void Unrate(Rating rating)
    {
        //try
        //{
        //    ValidationManager.Validate(rating);
        //}
        //catch (MultipleValidationsException)
        //{
        //    throw;
        //}

        if (_db.Ratings.GetUserRatingForAGame(rating.User, rating.Game) is not null)
        {
            _db.Ratings.Remove(rating);
        }

        _db.Save();
    }

    public void CreateUserCollection(UserCollection newCollection)
    {
        //try
        //{
        //    ValidationManager.Validate(newCollection);
        //}
        //catch (MultipleValidationsException)
        //{
        //    // logging?
        //    throw;
        //}

        _db.UserCollections.Add(newCollection);

        _db.Save();
    }

    public void AddGameToCollection(UserCollection userCollection, Game game)
    {
        var collection = _db.UserCollections.Get(userCollection.Id);

        // !!! Something's not right here... Need to check where this is a good idea
        collection?.Games.Add(game);

        _db.Save();
    }

    public void AddGameToCollection(DefaultCollection defaultCollection, Game game)
    {
        var collection = _db.DefaultCollections.Get(defaultCollection.Id);

        // !!! Something's not right here... Need to check where this is a good idea
        collection?.Games.Add(game);

        _db.Save();
    }

    public void RemoveGameFromCollection(UserCollection userCollection, Game game)
    {
        var collection = _db.UserCollections.Get(userCollection.Id);

        var gameToRemove = _db.Games.Get(game.Id);

        if (collection is null || gameToRemove is null)
        {
            throw new ArgumentException("Argument is null");
        }

        collection.Games.Remove(gameToRemove);

        _db.Update(collection);

        _db.Save();
    }

    public void RemoveGameFromCollection(DefaultCollection defaultCollection, Game game)
    {
        var defaultCollectionEntity = _db.UserCollections.Get(defaultCollection.Id);

        var gameEntityToRemove = _db.Games.Get(game.Id);

        if (defaultCollectionEntity is null || gameEntityToRemove is null)
        {
            throw new ArgumentException("Argument is null");
        }

        defaultCollectionEntity.Games.Remove(gameEntityToRemove);

        _db.Update(defaultCollectionEntity);

        _db.Save();
    }

    public void DeleteCollection(UserCollection userCollection)
    {
        _db.UserCollections.Remove(userCollection.Id);
        _db.Save();
    }

    public Rating? GetRatingForGame(User user, Game game)
    {
        var rating = _db.Ratings.GetAll(r => r.UserId == user.Id && r.GameId == game.Id)
            .SingleOrDefault();

        return rating;
    }

    public User UpdateUser(User user)
    {
        var actualUser = _db.Users.Get(user.Id);

        actualUser.About = user.About;

        actualUser.CountryId = user.CountryId;

        _db.Update(actualUser);

        _db.Save();

        return actualUser;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
