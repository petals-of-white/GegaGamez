using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class DefaultCollectionRepository : Repository<DefaultCollection>, IDefaultCollectionRepository
    {
        public DefaultCollectionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<DefaultCollection> GetByUser(User user) => GetAll(dc => dc.UserId == user.Id);
        public Task<IEnumerable<DefaultCollection>> GetByUserAsync(User user) => GetAllAsync(dc => dc.UserId == user.Id);
    }
}
