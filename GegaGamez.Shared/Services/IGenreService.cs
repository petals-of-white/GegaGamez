using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGenreService
{
    IEnumerable<Genre> FindAll();

    IEnumerable<Genre> FindByName(string genreName);

    Genre? GetById(int id);

    IEnumerable<Genre> GetGameGenres(Game game);
}
