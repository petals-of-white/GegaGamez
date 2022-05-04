using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGenreService
{
    IEnumerable<Genre> FindByName(string genreName);
    IEnumerable<Genre> GetAll();
}
