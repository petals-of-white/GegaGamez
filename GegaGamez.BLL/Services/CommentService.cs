using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _db;

    public CommentService(IUnitOfWork db)
    {
        _db = db;
    }

    public void AddComment(Comment newComment)
    {
        _db.Comments.Add(newComment);
        _db.Save();
    }

    public IEnumerable<Comment> FindAll() => _db.Comments.AsEnumerable();

    public Comment? GetById(int id) => _db.Comments.Get(id);

    public IEnumerable<Comment> GetGameComments(Game game) => _db.Comments.FindAll(c => c.GameId == game.Id);
}
