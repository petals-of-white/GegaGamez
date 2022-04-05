using GegaGamez.BLL.Models;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.Logic
{
    public class GameBL : IDisposable
    {
        private readonly IUnitOfWork _db;

        public GameBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public IEnumerable<Game> ListAll()
        {
            IEnumerable<Game> output;
            var games = _db.Games.List();

            output = AutoMapping.Mapper.Map<IEnumerable<Game>>(games);

            return output;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Game? GetById(int id)
        {
            var gameEntity = _db.Games.Get(id);

            var game = AutoMapping.Mapper.Map<Game>(gameEntity);

            // do the avg for score !!!
            if (gameEntity is not null)
            {
                game.AvgRatingScore = (byte) _db.Ratings.GetAverageGameRatingScore(gameEntity);
            }

            return game;
        }

        public IEnumerable<Game> FindByTitle(string title)
        {
            var gameEntitiesByTitle = _db.Games.GetAllByTitle(title);

            var games = AutoMapping.Mapper.Map<IEnumerable<Game>>(gameEntitiesByTitle);

            return games;
        }

        public IEnumerable<Game> GetByGenre(Genre genre)
        {
            IEnumerable<Game> output;
            var gamesByGenre = _db.Games.GetByGenre(AutoMapping.Mapper.Map<DAL.Entities.Genre>(genre));

            output = AutoMapping.Mapper.Map<IEnumerable<Game>>(gamesByGenre);

            return output;
        }
    }
}
