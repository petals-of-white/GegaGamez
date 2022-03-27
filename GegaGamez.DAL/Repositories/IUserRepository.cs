using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAllByUsername(string username);

        User? GetByCredentials(string username, string password);
    }
}
