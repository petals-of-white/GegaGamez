using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetGameComments(Game game);

        Task<IEnumerable<Comment>> GetGameCommentsAsync(Game game);

        IEnumerable<Comment> GetUserComments(User user);

        Task<IEnumerable<Comment>> GetUserCommentsAsync(User user);
    }
}
