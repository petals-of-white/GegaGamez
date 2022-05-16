using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class DeveloperService : IDisposable, IDeveloperService
{
    private readonly IUnitOfWork _db;

    public DeveloperService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<Developer> Find(string name) =>
        _db.Developers.FindAll(d => d.Name.ToLower().Contains(name.ToLower()));

    public IEnumerable<Developer> FindAll() => _db.Developers.AsEnumerable();

    public Developer? GetById(int id) => _db.Developers.Get(id);
}
