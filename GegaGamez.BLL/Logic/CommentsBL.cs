using GegaGamez.BLL.Models;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.Logic
{
    public class CommentsBL
    {
        private readonly IUnitOfWork _db;

        public CommentsBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public IEnumerable<Comment> GetCommentsByGame(Game game)
        {
            var gameEntity = AutoMapping.Mapper.Map<DAL.Entities.Game>(game);

            var comments = _db.Comments.GetGameComments(gameEntity);

            return AutoMapping.Mapper.Map<IEnumerable<Comment>>(comments);
        }
    }
}
