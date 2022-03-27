using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetUserComments(User user);
    }
}
