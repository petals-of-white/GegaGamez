using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetGameComments(Game game);

        Task<IEnumerable<Comment>> GetGameCommentsAsync(Game game);

        IEnumerable<Comment> GetUserComments(User user);

        Task<IEnumerable<Comment>> GetUserCommentsAsync(User user);
    }
}
