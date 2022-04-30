using GegaGamez.BLL.Enums;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Validation;

namespace GegaGamez.BLL.Services;

public class UserAuthService
{
    private readonly IUnitOfWork _db;

    public UserAuthService(string connectionString)
    {
        _db = new UnitOfWork(connectionString);
    }

    /// <summary>
    /// This method should be checked
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public UserAuthResult Authenticate(string username, string password)
    {
        UserAuthResult authResult;
        User? user = null;
        AuthStatus authStatus;

        try
        {
            var userEntityByCredentials = _db.Users.GetByCredentials(username, password);
            var userByCredentials = AutoMapping.Mapper.Map<User>(userEntityByCredentials);

            authStatus = AuthStatus.Success;
            user = userByCredentials;
            //return userByCredentials;
        }
        catch (ArgumentException)
        {
            authStatus = AuthStatus.IncorrectPassword;
            //throw;
        }

        authResult = new UserAuthResult(user, authStatus);

        return authResult;
    }

    /// <summary>
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="name"></param>
    /// <param name="country"></param>
    /// <param name="about"></param>
    /// <returns></returns>
    /// <exception cref="MultipleValidationsException"></exception>
    public UserAuthResult CreateNewUser(string username, string password, string? name = null, Country? country = null, string? about = null)
    {
        User newUser = new()
        {
            Username = username,
            Password = password,
            Name = name,
            Country = country,
            About = about,
        };

        return CreateNewUser(newUser);
    }

    /// <summary>
    /// </summary>
    /// <param name="newUser"></param>
    /// <returns></returns>
    /// <exception cref="MultipleValidationsException"></exception>

    public UserAuthResult CreateNewUser(User newUser)
    {
        UserAuthResult authResult;

        try
        {
            ValidationManager.Validate(newUser);
        }
        catch (MultipleValidationsException)
        {
            throw;
        }

        ICollection<DefaultCollection> userDefaultCollections = new HashSet<DefaultCollection>();

        // adding default collections for user
        var defaultCollectionTypes = AutoMapping.Mapper
                .Map<IEnumerable<DefaultCollectionType>>(_db.DefaultCollectionTypes.List());

        foreach (var collectionType in defaultCollectionTypes)
        {
            userDefaultCollections.Add(new DefaultCollection() { DefaultCollectionType = collectionType });
        }

        newUser.DefaultCollections = userDefaultCollections;

        // map
        var userEntity = AutoMapping.Mapper.Map<Shared.Entities.User>(newUser);

        _db.Users.Add(userEntity);

        _db.Save();

        // map back
        newUser = AutoMapping.Mapper.Map<User>(userEntity);

        authResult = new UserAuthResult(newUser, AuthStatus.Success);

        return authResult;
    }
}
