using System.Linq.Expressions;
using EntityFramework.Exceptions.Common;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.BLL.Services;

public class CommentService : ICommentService, IDisposable
{
    private readonly IUnitOfWork _db;
    private readonly Expression<Func<Comment, object>> [] _commentInclues = { c => c.User, c => c.Game };

    public CommentService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void AddComment(Comment newComment)
    {
        newComment.CreatedAt = DateTime.Now;

        try
        {
            _db.Comments.Add(newComment);

            _db.Save();
        }
        //catch(EntityFramework.Exceptions.Common.ReferenceConstraintException)
        //{
        //}
        catch (UniqueConstraintException ex)
        {
            throw new UniqueEntityException(newComment, ex);
        }
    }

    public void DeleteComment(Comment actualComment)
    {
        try
        {
            _db.Comments.Remove(actualComment);
            _db.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            string? msg = "Failed to remove Comment with id because it doesn't exist. See the inner exception for details.";
            throw new EntityNotFoundException(msg, ex);
        }
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<Comment> FindAll() => _db.Comments.AsEnumerable(_commentInclues);

    public Comment? GetById(int id) => _db.Comments.Get(id, _commentInclues);

    public IEnumerable<Comment> GetGameComments(Game game) =>
        _db.Comments.FindAll(c => c.GameId == game.Id, _commentInclues);
}
