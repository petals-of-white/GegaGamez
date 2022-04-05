using System.ComponentModel.DataAnnotations;
using GegaGamez.BLL.Exceptions;
using GegaGamez.BLL.Models;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;
using Microsoft.EntityFrameworkCore;

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

        public User CreateNewUser(string username, string password, string? name = null, Country? country = null, string? about = null)
        {
            throw new NotImplementedException();

            User newUser;
            ICollection<DefaultCollection> userDefaultCollections = new HashSet<DefaultCollection>();

            newUser = new()
            {
                Username = username,
                Password = password,
                Name = name,
                Country = country,
                About = about,
            };

            try
            {
                ValidationManager.Validate(newUser);

                var defaultCollectionTypes = AutoMapping.Mapper
                    .Map<IEnumerable<DefaultCollectionType>>(_db.DefaultCollectionTypes.List());

                foreach (var collectionType in defaultCollectionTypes)
                {
                    userDefaultCollections.Add(new DefaultCollection() { DefaultCollectionType = collectionType });
                }

                return newUser;
            }
            catch (MultipleValidationsException)
            {
                throw;
            }
        }

        public void CreateNewUser(User newUser)
        {
            throw new NotImplementedException();

            var validationErrors = (ICollection<ValidationResult>) newUser.Validate(new ValidationContext(newUser));
            if (validationErrors.Any())
            {
                var validationErrorsException = new MultipleValidationsException("Wrong", validationErrors);
                throw validationErrorsException;
            }

            try
            {
                var userEntity = AutoMapping.Mapper.Map<DAL.Entities.User>(newUser);

                _db.Users.Add(userEntity);

                _db.Save();

                newUser = AutoMapping.Mapper.Map<User>(userEntity);
            }
            catch (DbUpdateException ex)
            {
            }
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
            catch (ArgumentException ex)
            {
                throw;
            }
        }
    }
}
