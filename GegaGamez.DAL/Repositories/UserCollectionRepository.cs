using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class UserCollectionRepository : Repository<UserCollection>, IUserCollectionRepository
{
    //protected override IQueryable<UserCollection> DbSetWithIncludedProperties => _dbSet.Include(uc => uc.User);

    public UserCollectionRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //public override UserCollection? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(uc => uc.Id == id);

    //public override Task<UserCollection?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(uc => uc.Id == id);
}
