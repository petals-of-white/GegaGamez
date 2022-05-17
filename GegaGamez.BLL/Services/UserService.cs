using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class UserService : IDisposable, IUserService
{
    private readonly IUnitOfWork _db;

    public UserService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Create(User newUser)
    {
        // adding default collections for user
        ICollection<DefaultCollection> userDefaultCollections = new HashSet<DefaultCollection>();

        var defaultCollectionTypes = _db.DefaultCollectionTypes.AsEnumerable();

        foreach (var collectionType in defaultCollectionTypes)
        {
            userDefaultCollections.Add(new DefaultCollection() { DefaultCollectionType = collectionType });
        }

        newUser.DefaultCollections = userDefaultCollections;
        newUser.UserCollections = new HashSet<UserCollection>();

        Role userRole = _db.Roles.FindAll(r => r.Name == "User").First();
        newUser.Roles = new HashSet<Role>() { userRole };

        try
        {
            _db.Users.Add(newUser);
            _db.Save();
        }
        catch (Exception)
        {
            // log ?
            throw;
        }
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<User> Find(string username) =>
        _db.Users.FindAll(u => u.Username.ToLower().Contains(username.ToLower()));

    public IEnumerable<User> GetAll() => _db.Users.AsEnumerable();

    public User? GetById(int id) => _db.Users.Get(id);

    public User? GetByUsername(string username) =>
        _db.Users.FindAll(u => u.Username == username).SingleOrDefault();

    public User UpdateUser(User user)
    {
        // Think of a better way to do this
        var actualUser = _db.Users.Get(user.Id);

        actualUser.About = user.About;

        actualUser.CountryId = user.CountryId;

        _db.Update(actualUser);

        _db.Save();

        return actualUser;
    }
}
