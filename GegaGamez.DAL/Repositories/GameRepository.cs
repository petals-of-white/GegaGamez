using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class GameRepository : Repository<Game>, IGameRepository
{
    public GameRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
