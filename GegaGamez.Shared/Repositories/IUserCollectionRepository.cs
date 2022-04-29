using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Repositories
{
    public interface IUserCollectionRepository : IRepository<UserCollection>
    {
        IEnumerable<UserCollection> GetAllByUser(User user);

        Task<IEnumerable<UserCollection>> GetAllByUserAsync(User user);
    }
}
