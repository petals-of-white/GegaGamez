using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;

namespace GegaGamez.BLL.Services;

public class GenreService : IDisposable
{
    private readonly IUnitOfWork _db;

    public GenreService(IUnitOfWork db)
    {
        _db = db;
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
