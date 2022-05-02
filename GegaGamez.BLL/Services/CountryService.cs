using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork _db;

    public CountryService(IUnitOfWork db)
    {
        _db = db;
    }

    public IEnumerable<Country> AllCountries() => _db.Countries.List();
}
