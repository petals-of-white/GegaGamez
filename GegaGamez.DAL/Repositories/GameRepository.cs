using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class GameRepository : Repository<Game>, IGameRepository
{
    protected override IQueryable<Game> DbSetWithIncludedProperties => _dbSet.Include(g => g.Developer);

    public GameRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public override Game? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(g => g.Id == id);

    public override Task<Game?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(g => g.Id == id);

}
