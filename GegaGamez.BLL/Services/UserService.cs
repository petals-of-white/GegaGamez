using GegaGamez.BLL.Enums;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;
using GegaGamez.Shared.BusinessModels;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Validation;

namespace GegaGamez.BLL.Services
{
    public class UserService : IDisposable
    {
        private readonly IUnitOfWork _db;

        public UserService(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public User? GetById(int id)
        {
            var userEntity = _db.Users.Get(id);
            var user = AutoMapping.Mapper.Map<User>(userEntity);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var allUsers = _db.Users.List();
            return AutoMapping.Mapper.Map<IEnumerable<User>>(allUsers);
        }

        public IEnumerable<User> FindByUsername(string username)
        {
            var usersEntitiesByUsername = _db.Users.GetAllByUsername(username);

            var usersByUsername = AutoMapping.Mapper.Map<IEnumerable<User>>(usersEntitiesByUsername);

            return usersByUsername;
        }

        public void LoadUsersCollections(User user)
        {
            user.DefaultCollections = AutoMapping.Mapper.Map<IEnumerable<DefaultCollection>>
                (_db.DefaultCollections
                .GetByUser(AutoMapping.Mapper.Map<DAL.Entities.User>(user))).ToList();

            user.UserCollections = AutoMapping.Mapper.Map<IEnumerable<UserCollection>>
                (_db.UserCollections
                .GetAllByUser(AutoMapping.Mapper.Map<DAL.Entities.User>(user))).ToList();
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
        /// <param name="rating"></param>
        /// <exception cref="MultipleValidationsException"></exception>
        public void RateGame(Rating rating)
        {
            try
            {
                ValidationManager.Validate(rating);
            }
            catch (MultipleValidationsException)
            {
                throw;
            }

            var ratingEntity = AutoMapping.Mapper.Map<DAL.Entities.Rating>(rating);

            // check if user rating for a game already exists
            bool ratingExist = _db.Ratings.GetAll(
                r => r.UserId == ratingEntity.UserId
                && r.GameId == ratingEntity.GameId)
                .Count() == 1;

            if (ratingExist)
            {
                _db.Update(ratingEntity);
            }
            else
            {
                _db.Ratings.Add(ratingEntity);
            }

            _db.Save();
        }

        public void Unrate(Rating rating)
        {
            try
            {
                ValidationManager.Validate(rating);
            }
            catch (MultipleValidationsException)
            {
                throw;
            }

            var ratingEntity = AutoMapping.Mapper.Map<DAL.Entities.Rating>(rating);

            if (_db.Ratings.Get(rating.Id) is not null)
            {
                _db.Ratings.Remove(ratingEntity);
            }

            _db.Save();
        }

        public void CreateUserCollection(UserCollection newCollection)
        {
            try
            {
                ValidationManager.Validate(newCollection);
            }
            catch (MultipleValidationsException)
            {
                // logging?
                throw;
            }

            var collectionEntity = AutoMapping.Mapper.Map<DAL.Entities.UserCollection>(newCollection);

            _db.UserCollections.Add(collectionEntity);

            _db.Save();
        }

        public void AddGameToCollection(UserCollection userCollection, Game game)
        {
            var gameEntity = AutoMapping.Mapper.Map<DAL.Entities.Game>(game);

            var userCollectionEntity = _db.UserCollections.Get(userCollection.Id);

            // !!! Something's not right here... Need to check where this is a good idea
            userCollectionEntity?.Games.Add(gameEntity);

            _db.Save();
        }

        public void AddGameToCollection(DefaultCollection defaultCollection, Game game)
        {
            var gameEntity = AutoMapping.Mapper.Map<DAL.Entities.Game>(game);

            var defaultCollectionEntity = _db.DefaultCollections.Get(defaultCollection.Id);

            // !!! Something's not right here... Need to check where this is a good idea
            defaultCollectionEntity?.Games.Add(gameEntity);

            _db.Save();
        }

        public void RemoveGameFromCollection(UserCollection userCollection, Game game)
        {
            var userCollectionEntity = _db.UserCollections.Get(userCollection.Id);

            var gameEntityToRemove = _db.Games.Get(game.Id);

            if (userCollectionEntity is null || gameEntityToRemove is null)
            {
                throw new ArgumentException("Argument is null");
            }

            userCollectionEntity.Games.Remove(gameEntityToRemove);

            _db.Update(userCollectionEntity);

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

        public void DeleteCollection(UserCollection defaultCollection)
        {
            _db.UserCollections.Remove(defaultCollection.Id);
            _db.Save();
        }

        public Rating? GetRatingForGame(User user, Game game)
        {
            var ratingEntity = _db.Ratings.GetAll(r => r.UserId == user.Id && r.GameId == game.Id)
                .SingleOrDefault();

            return AutoMapping.Mapper.Map<Rating>(ratingEntity);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
