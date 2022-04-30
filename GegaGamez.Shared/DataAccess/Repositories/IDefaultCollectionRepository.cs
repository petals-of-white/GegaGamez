using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.DataAccess.Repositories
{
    public interface IDefaultCollectionRepository : IRepository<DefaultCollection>
    {
        IEnumerable<DefaultCollection> GetByUser(User user);

        Task<IEnumerable<DefaultCollection>> GetByUserAsync(User user);
    }
}
