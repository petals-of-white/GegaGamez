using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IDefaultCollectionRepository : IRepository<DefaultCollection>
    {
        IEnumerable<DefaultCollection> GetByUser(User user);
    }
}
