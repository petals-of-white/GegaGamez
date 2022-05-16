using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface ICountryService
{
    IEnumerable<Country> FindAll();

    Country? GetById(int id);
}
