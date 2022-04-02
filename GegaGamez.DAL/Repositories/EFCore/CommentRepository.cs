using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        protected override IQueryable<Comment> DbSetWithIncludedProperties => _dbSet.Include(c => c.Game).Include(c => c.User);

        public CommentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Comment> GetUserComments(User user) => GetAll(c => c.UserId == user.Id);

        public Task<IEnumerable<Comment>> GetUserCommentsAsync(User user) => GetAllAsync(c => c.UserId == user.Id);

        public IEnumerable<Comment> GetGameComments(Game game) => GetAll(c => c.GameId == game.Id);

        public Task<IEnumerable<Comment>> GetGameCommentsAsync(Game game) => GetAllAsync(c => c.GameId == game.Id);
    }
}
