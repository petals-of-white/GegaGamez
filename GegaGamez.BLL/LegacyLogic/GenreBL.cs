using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;
using GegaGamez.Shared.BusinessModels;

namespace GegaGamez.BLL.LegacyLogic
{
    public class GenreBL : IDisposable

    {
        private readonly IUnitOfWork _db;

        public GenreBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public IEnumerable<Genre> ListAll()
        {
            IEnumerable<Genre> output;

            var allGenres = _db.Genres.List();

            output = AutoMapping.Mapper.Map<IEnumerable<Genre>>(allGenres);

            return output;
        }

        public IEnumerable<Genre> GetGamesGenres(Game game)
        {
            IEnumerable<Genre> output;

            var gameEntity = AutoMapping.Mapper.Map<DAL.Entities.Game>(game);

            output = AutoMapping.Mapper.Map<IEnumerable<Genre>>(_db.Genres.GetGamesGenres(gameEntity));

            return output;
        }

        public IEnumerable<Genre> FindByName(string genreName)
        {
            IEnumerable<Genre> output;

            var genresByName = _db.Genres.GetAllByName(genreName);

            output = AutoMapping.Mapper.Map<IEnumerable<Genre>>(genresByName);

            return output;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
