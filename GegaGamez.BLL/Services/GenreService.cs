using GegaGamez.Shared.BusinessModels;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.Services
{
    public class GenreService : IDisposable
    {
        private readonly IUnitOfWork _db;

        public GenreService(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public IEnumerable<Genre> GetAll()
        {
            IEnumerable<Genre> output;

            var allGenres = _db.Genres.List();

            output = AutoMapping.Mapper.Map<IEnumerable<Genre>>(allGenres);

            return output;
        }

        public IEnumerable<Genre> FindByName(string genreName)
        {
            throw new NotImplementedException();

            //IEnumerable<Genre> output;

            //var genresByName = _db.Genres.GetAllByName(genreName);

            //output = AutoMapping.Mapper.Map<IEnumerable<Genre>>(genresByName);

            //return output;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
