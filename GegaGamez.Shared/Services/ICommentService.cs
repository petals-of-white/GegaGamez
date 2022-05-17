using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface ICommentService
{
    void AddComment(Comment newComment);

    public IEnumerable<Comment> FindAll();

    public Comment? GetById(int id);

    public IEnumerable<Comment> GetGameComments(Game game);
}
