using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class DefaultCollectionRepository : Repository<DefaultCollection>, IDefaultCollectionRepository
{
    //protected override IQueryable<DefaultCollection> DbSetWithIncludedProperties => _dbSet.Include(dc => dc.User).Include(dc => dc.DefaultCollectionType);

    public DefaultCollectionRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //public override DefaultCollection? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(dc => dc.Id == id);

    //public override Task<DefaultCollection?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(dc => dc.Id == id);
}
