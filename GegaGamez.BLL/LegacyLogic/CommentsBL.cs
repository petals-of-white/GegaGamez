using GegaGamez.DAL.Services.EFCore;
using GegaGamez.Shared.BusinessModels;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.LegacyLogic
{
    public class CommentsBL : IDisposable
    {
        private readonly IUnitOfWork _db;

        public CommentsBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public IEnumerable<Comment> GetCommentsByGame(Game game)
        {
            var gameEntity = AutoMapping.Mapper.Map<Shared.Entities.Game>(game);

            var comments = _db.Comments.GetGameComments(gameEntity);

            return AutoMapping.Mapper.Map<IEnumerable<Comment>>(comments);
        }

        public void AddComment(Comment comment)
        {
            var commentEntity = AutoMapping.Mapper.Map<Shared.Entities.Comment>(comment);

            _db.Comments.Add(commentEntity);

            _db.Save();
        }

        public void RemoveComment(Comment comment)
        {
            var commentEntity = AutoMapping.Mapper.Map<Shared.Entities.Comment>(comment);

            _db.Comments.Remove(commentEntity);

            _db.Save();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
