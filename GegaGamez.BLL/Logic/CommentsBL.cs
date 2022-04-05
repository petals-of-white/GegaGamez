﻿using GegaGamez.BLL.Models;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.Logic
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
            var gameEntity = AutoMapping.Mapper.Map<DAL.Entities.Game>(game);

            var comments = _db.Comments.GetGameComments(gameEntity);

            return AutoMapping.Mapper.Map<IEnumerable<Comment>>(comments);
        }

        public void AddComment(Comment comment)
        {
            var commentEntity = AutoMapping.Mapper.Map<DAL.Entities.Comment>(comment);

            _db.Comments.Add(commentEntity);

            _db.Save();
        }

        public void RemoveComment(Comment comment)
        {
            var commentEntity = AutoMapping.Mapper.Map<DAL.Entities.Comment>(comment);

            _db.Comments.Remove(commentEntity);

            _db.Save();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
