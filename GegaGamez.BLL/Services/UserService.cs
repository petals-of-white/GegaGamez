using System.Linq.Expressions;
using EntityFramework.Exceptions.Common;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.BLL.Services;

public class UserService : IDisposable, IUserService
{
    private readonly IUnitOfWork _db;
    private readonly Expression<Func<User, object>> [] _userIncludes = { u => u.Country!, u => u.Roles };

    /// <summary>
    /// Generate default collections, usually 4 of them
    /// </summary>
    /// <returns></returns>
    private ICollection<DefaultCollection> GenerateDefaultCollections()
    {
        ICollection<DefaultCollection> userDefaultCollections = new HashSet<DefaultCollection>();

        var defaultCollectionTypes = _db.DefaultCollectionTypes.AsEnumerable();

        foreach (var collectionType in defaultCollectionTypes)
        {
            userDefaultCollections.Add(new DefaultCollection() { DefaultCollectionType = collectionType });
        }
        return userDefaultCollections;
    }

    private ICollection<Role> GetRole(string role) =>
            _db.Roles.FindAll(r => r.Name == role).ToHashSet();

    public UserService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void CreateUser(User newUser)
    {
        newUser.DefaultCollections = GenerateDefaultCollections();
        newUser.UserCollections = new HashSet<UserCollection>();
        newUser.Roles = GetRole("User");

        try
        {
            _db.Users.Add(newUser);
            _db.Save();
        }
        catch (UniqueConstraintException ex)
        {
            throw new EntityNotFoundException(newUser, ex);
        }
    }

    public void DeleteUser(User user)
    {
        try
        {
            _db.Users.Remove(user);
            _db.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityNotFoundException(user, ex);
        }
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<User> Find(string username) =>
        _db.Users.FindAll(u => u.Username.ToLower().Contains(username.ToLower()), _userIncludes);

    public IEnumerable<User> GetAll() => _db.Users.AsEnumerable(_userIncludes);

    public User? GetById(int id) => _db.Users.Get(id, _userIncludes);

    public User? GetByUsername(string username) =>
        _db.Users.FindAll(u => u.Username == username, _userIncludes).SingleOrDefault();

    public void UpdateUser(User user)
    {
        var actualUser = _db.Users.Get(user.Id)
            ?? throw new EntityNotFoundException(user, null);


        var actualCountry = user.CountryId.HasValue
            ? _db.Countries.Get(user.CountryId.Value)
            : null;


        actualUser.About = user.About;

        if (user.CountryId.HasValue)
        {
            if (actualCountry is not null)
                actualUser.CountryId = actualCountry.Id;
            else
                throw new EntityNotFoundException(new Country { Id = user.CountryId.Value }, null);
        }

        try
        {
            //_db.Update(actualUser);
            _db.Save();
        }
        catch (UniqueConstraintException ex)
        {
            var msg = $"Can Update User {user.Id} because it or it's member violate unique constraint";
            throw new UniqueEntityException(msg, ex);
        }
    }
}
