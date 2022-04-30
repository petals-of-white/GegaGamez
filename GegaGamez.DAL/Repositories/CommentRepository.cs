using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    protected override IQueryable<Comment> DbSetWithIncludedProperties => _dbSet.Include(c => c.Game).Include(c => c.User);

    public CommentRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public override Comment? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(c => c.Id == id);

    public override Task<Comment?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(c => c.Id == id);

    public IEnumerable<Comment> GetGameComments(Game game) => GetAll(c => c.GameId == game.Id);

    public Task<IEnumerable<Comment>> GetGameCommentsAsync(Game game) => GetAllAsync(c => c.GameId == game.Id);

    public IEnumerable<Comment> GetUserComments(User user) => GetAll(c => c.UserId == user.Id);

    public Task<IEnumerable<Comment>> GetUserCommentsAsync(User user) => GetAllAsync(c => c.UserId == user.Id);
}
