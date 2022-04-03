using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class DefaultCollectionRepository : Repository<DefaultCollection>, IDefaultCollectionRepository
    {
        protected override IQueryable<DefaultCollection> DbSetWithIncludedProperties => _dbSet.Include(dc => dc.User).Include(dc => dc.DefaultCollectionType);

        public DefaultCollectionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override DefaultCollection? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(dc => dc.Id == id);

        public override Task<DefaultCollection?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(dc => dc.Id == id);

        public IEnumerable<DefaultCollection> GetByUser(User user) => GetAll(dc => dc.UserId == user.Id);

        public Task<IEnumerable<DefaultCollection>> GetByUserAsync(User user) => GetAllAsync(dc => dc.UserId == user.Id);
    }
}
