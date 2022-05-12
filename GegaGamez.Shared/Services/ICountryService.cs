using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface ICountryService
{
    Country? GetById(int id);
    public IEnumerable<Country> FindAll();
}
