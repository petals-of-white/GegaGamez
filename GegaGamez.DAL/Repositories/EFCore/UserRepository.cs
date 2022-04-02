using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        protected override IQueryable<User> DbSetWithIncludedProperties => _dbSet.Include(u => u.Country);

        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<User> GetAllByUsername(string username)
        {
            var usersByUsername = (from user in DbSetWithIncludedProperties
                                   where user.Username.Contains(username, StringComparison.OrdinalIgnoreCase)
                                   select user).ToList();

            return usersByUsername;
        }

        public async Task<IEnumerable<User>> GetAllByUsernameAsync(string username)
        {
            var usersByUsername = await (from user in DbSetWithIncludedProperties
                                         where user.Username.Contains(username, StringComparison.OrdinalIgnoreCase)
                                         select user).ToListAsync();

            return usersByUsername;
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public User? GetByCredentials(string username, string password)
        {
            User? user = DbSetWithIncludedProperties.SingleOrDefault(u => u.Username == username);

            return user ?? throw new ArgumentException("User found, but the password was incorrect.", nameof(password));
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<User?> GetByCredentialsAsync(string username, string password)
        {
            User? user = await DbSetWithIncludedProperties.SingleOrDefaultAsync(u => u.Username == username);

            return user ?? throw new ArgumentException("User found, but the password was incorrect.", nameof(password));
        }
    }
}
