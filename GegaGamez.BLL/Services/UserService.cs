using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class UserService : IDisposable, IUserService
{
	private readonly IUnitOfWork _db;

	private ICollection<Role> AddRoles(string Role) =>
		_db.Roles.FindAll(r => r.Name == Role).ToHashSet();

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

	public UserService(IUnitOfWork db)
	{
		_db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
	}

	public void CreateUser(User newUser)
	{
		newUser.DefaultCollections = GenerateDefaultCollections();
		newUser.UserCollections = new HashSet<UserCollection>();
		newUser.Roles = AddRoles("User");

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

	public void DeleteUser(User user)
	{
		_db.Users.Remove(user);
		_db.Save();
	}

	public void Dispose() => _db.Dispose();

	public IEnumerable<User> Find(string username) =>
		_db.Users.FindAll(u => u.Username.ToLower().Contains(username.ToLower()));

	public IEnumerable<User> GetAll() => _db.Users.AsEnumerable();

	public User? GetById(int id) => _db.Users.Get(id);

	public User? GetByUsername(string username) =>
		_db.Users.FindAll(u => u.Username == username).SingleOrDefault();

	public void UpdateUser(User user)
	{
		var actualUser = _db.Users.Get(user.Id);

		if (actualUser is null)
			throw new KeyNotFoundException("User wasn't found");
		else
		{
			actualUser.About = user.About;
			actualUser.CountryId = user.CountryId;
			actualUser.Country = user.Country;

			_db.Update(actualUser);
		}

		_db.Save();
	}
}
