using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class GenreService : IDisposable, IGenreService
{
    private readonly IUnitOfWork _db;
    private readonly Expression<Func<Genre, object>> [] _genreIncludes = { };

    public GenreService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<Genre> FindAll() => _db.Genres.AsEnumerable(_genreIncludes);

    public IEnumerable<Genre> FindByName(string genreName) =>
        _db.Genres.FindAll(g => g.Name.ToLower().Contains(genreName.ToLower()), _genreIncludes);

    public Genre? GetById(int id) => _db.Genres.Get(id, _genreIncludes);

    public IEnumerable<Genre> GetGameGenres(Game game) =>
        _db.Genres.FindAll(genre => genre.Games.Select(g => g.Id).Contains(game.Id), _genreIncludes);
}
