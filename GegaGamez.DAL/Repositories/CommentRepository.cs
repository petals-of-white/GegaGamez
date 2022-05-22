using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    //protected override IQueryable<Comment> DbSetWithIncludedProperties => _dbSet.Include(c => c.Game).Include(c => c.User);

    public CommentRepository(DbContext dbContext) : base(dbContext)
    {
    }

    //public override Comment? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(c => c.Id == id);

    //public override Task<Comment?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(c => c.Id == id);
}
