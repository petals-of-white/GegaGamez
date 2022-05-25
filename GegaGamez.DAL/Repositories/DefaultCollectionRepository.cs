using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class DefaultCollectionRepository : Repository<DefaultCollection>, IDefaultCollectionRepository
{
    public DefaultCollectionRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
