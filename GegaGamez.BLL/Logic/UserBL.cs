using GegaGamez.BLL.Models;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Validation;

namespace GegaGamez.BLL.Logic
{
    public class UserBL : IDisposable
    {
        private readonly IUnitOfWork _db;

        public UserBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public User? GetById(int id)
        {
            var userEntity = _db.Users.Get(id);
            var user = AutoMapping.Mapper.Map<User>(userEntity);

            return user;
        }

        public IEnumerable<User> ListAll()
        {
            var allUsers = _db.Users.List();
            return AutoMapping.Mapper.Map<IEnumerable<User>>(allUsers);
        }

        public User CreateNewUser(string username, string password, string? name = null, Country? country = null, string? about = null)
        {
            User newUser;

            newUser = new()
            {
                Username = username,
                Password = password,
                Name = name,
                Country = country,
                About = about,
            };

            return CreateNewUser(newUser);
        }

        public User CreateNewUser(User newUser)
        {
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
            var userEntity = AutoMapping.Mapper.Map<DAL.Entities.User>(newUser);

            _db.Users.Add(userEntity);

            _db.Save();

            // map back
            newUser = AutoMapping.Mapper.Map<User>(userEntity);

            return newUser;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IEnumerable<User> FindByUsername(string username)
        {
            var usersEntitiesByUsername = _db.Users.GetAllByUsername(username);

            var usersByUsername = AutoMapping.Mapper.Map<IEnumerable<User>>(usersEntitiesByUsername);

            return usersByUsername;
        }

        public User? GetByCredentials(string username, string password)
        {
            try
            {
                var userEntityByCredentials = _db.Users.GetByCredentials(username, password);
                var userByCredentials = AutoMapping.Mapper.Map<User>(userEntityByCredentials);
                return userByCredentials;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }
    }
}
