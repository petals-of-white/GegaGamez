using System.Linq.Expressions;
using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class DefaultCollectionRepository : Repository<DefaultCollection>, IDefaultCollectionRepository
    {
        public DefaultCollectionRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
