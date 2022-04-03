using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllByUsername(string username);

        Task<IEnumerable<User>> GetAllByUsernameAsync(string username);

        User? GetByCredentials(string username, string password);

        Task<User?> GetByCredentialsAsync(string username, string password);
    }
}
