using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    protected override IQueryable<User> DbSetWithIncludedProperties => _dbSet.Include(u => u.Country).Include(u => u.Roles);

    public UserRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public override User? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(u => u.Id == id);

    public override Task<User?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(u => u.Id == id);
}
