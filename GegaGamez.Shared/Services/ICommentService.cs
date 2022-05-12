using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface ICommentService
{
    public Comment? GetById(int id);

    public IEnumerable<Comment> FindAll();

    public IEnumerable<Comment> GetGameComments(Game game);

    void AddComment(Comment newComment);
}
