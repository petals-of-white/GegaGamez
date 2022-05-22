using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class CommentService : ICommentService, IDisposable
{
    private readonly IUnitOfWork _db;
    private Expression<Func<Comment, object>> [] _commentInclues = { c=>c.User, c=>c.Game};

    public CommentService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void AddComment(Comment newComment)
    {
        newComment.CreatedAt = DateTime.Now;
        _db.Comments.Add(newComment);
        _db.Save();
    }

    public void DeleteComment(Comment actualComment)
    {
        _db.Comments.Remove(actualComment);
        _db.Save();
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<Comment> FindAll() => _db.Comments.AsEnumerable(_commentInclues);

    public Comment? GetById(int id) => _db.Comments.Get(id, _commentInclues);

    public IEnumerable<Comment> GetGameComments(Game game) =>
        _db.Comments.FindAll(c => c.GameId == game.Id, _commentInclues);
}
