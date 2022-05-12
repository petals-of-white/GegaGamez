using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGenreService
{
    Genre? GetById(int id);
    IEnumerable<Genre> FindByName(string genreName);
    IEnumerable<Genre> FindAll();
}
