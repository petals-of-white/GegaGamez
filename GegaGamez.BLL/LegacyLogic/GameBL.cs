using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;
using GegaGamez.Shared.BusinessModels;

namespace GegaGamez.BLL.LegacyLogic
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
            ICollection<Game> output = new List<Game>();
            var games = _db.Games.List();

            //output = AutoMapping.Mapper.Map<IEnumerable<Game>>(games);

            foreach (var gameEntity in games)
            {
                var game = AutoMapping.Mapper.Map<Game>(gameEntity);
                game.AvgRatingScore = (byte) _db.Ratings.GetAverageGameRatingScore(gameEntity);
                output.Add(game);
            }

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
            ICollection<Game> output = new List<Game>();
            var gameEntitiesByTitle = _db.Games.GetAllByTitle(title);

            foreach (var gameEntity in gameEntitiesByTitle)
            {
                var gameByTitle = AutoMapping.Mapper.Map<Game>(gameEntity);
                gameByTitle.AvgRatingScore = (byte) _db.Ratings.GetAverageGameRatingScore(gameEntity);

                output.Add(gameByTitle);
            }

            return output;
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
