using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class DeveloperService : IDisposable, IDeveloperService
{
    private readonly IUnitOfWork _db;
    private readonly Expression<Func<Developer, object>> [] _devIncludes = {};

    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public DeveloperService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<Developer> Find(string name) =>
        _db.Developers.FindAll(d => d.Name.ToLower().Contains(name.ToLower()), _devIncludes);

    public IEnumerable<Developer> FindAll() => _db.Developers.AsEnumerable(_devIncludes);

    public Developer? GetById(int id) => _db.Developers.Get(id, _devIncludes);
}
