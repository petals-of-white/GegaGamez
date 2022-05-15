using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork _db;

    public CountryService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public Country? GetById(int id) => _db.Countries.Get(id);

    public IEnumerable<Country> FindAll() => _db.Countries.AsEnumerable();
}
