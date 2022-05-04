using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class GenreService : IDisposable, IGenreService
{
    private readonly IUnitOfWork _db;

    public GenreService(IUnitOfWork db)
    {
        _db = db;
    }

    public IEnumerable<Genre> GetAll() => _db.Genres.AsEnumerable();

    public IEnumerable<Genre> FindByName(string genreName)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
