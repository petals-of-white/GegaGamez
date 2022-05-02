using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface ICountryService
{
    public IEnumerable<Country> AllCountries();
}
