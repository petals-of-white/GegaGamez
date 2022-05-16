using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class DeveloperRepository : Repository<Developer>, IDeveloperRepository
{
    public DeveloperRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
