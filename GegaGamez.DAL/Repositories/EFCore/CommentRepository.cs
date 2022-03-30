using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Comment> GetUserComments(User user) => GetAll(c => c.UserId == user.Id);

        public Task<IEnumerable<Comment>> GetUserCommentsAsync(User user) => GetAllAsync(c => c.UserId == user.Id);
    }
}
