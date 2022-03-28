using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<User> GetAllByUsername(string username)
        {
            // this anonymous comparer method might be replaced by something better in the future
            var usernameSearcher = delegate (string inputUsername, string compareToUsername)
            {
                return compareToUsername.ToLower().Contains(inputUsername.ToLower());
            };

            var usersByUsername = (from user in _dbSet
                                   where usernameSearcher(username, user.Username)
                                   select user).ToList();

            return usersByUsername;
        }

        public async Task<IEnumerable<User>> GetAllByUsernameAsync(string username)
        {
            // this anonymous comparer method might be replaced by something better in the future
            var usernameSearcher = delegate (string inputUsername, string compareToUsername)
            {
                return compareToUsername.ToLower().Contains(inputUsername.ToLower());
            };

            var usersByUsername = await (from user in _dbSet
                                         where usernameSearcher(username, user.Username)
                                         select user).ToListAsync();

            var task = await Task.FromResult(usersByUsername.AsEnumerable());

            return task;
        }

        public User? GetByCredentials(string username, string password)
        {
            User? user = _dbSet.SingleOrDefault(u => u.Username == username);

            return user ?? throw new ArgumentException("User found, but the password was incorrect.", nameof(password));
        }

        public async Task<User?> GetByCredentialsAsync(string username, string password)
        {
            User? user = await _dbSet.SingleOrDefaultAsync(u => u.Username == username);

            return user ?? throw new ArgumentException("User found, but the password was incorrect.", nameof(password));
        }
    }
}
