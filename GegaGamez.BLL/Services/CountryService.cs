using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class CountryService : ICountryService, IDisposable
{
    private readonly IUnitOfWork _db;

    public CountryService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<Country> FindAll() => _db.Countries.AsEnumerable();

    public Country? GetById(int id) => _db.Countries.Get(id);
}
