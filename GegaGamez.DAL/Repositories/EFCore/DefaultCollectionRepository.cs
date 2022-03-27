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
    }
}
