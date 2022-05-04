using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class DeveloperService : IDisposable, IDeveloperService
{
    private readonly IUnitOfWork _db;

    public DeveloperService(IUnitOfWork db)
    {
        _db = db;
    }

    public IEnumerable<Developer> FindAll() => _db.Developers.AsEnumerable();

    public Developer? GetById(int id) => _db.Developers.Get(id);

    public IEnumerable<Developer> Find(string name)
    {
        var developersByName = _db.Developers.FindAll(d => d.Name.ToLower().Contains(name.ToLower()));
        return developersByName;
    }

    public void LoadGames(Developer developer)
    {
        var games = _db.Games.FindAll(g => g.DeveloperId == developer.Id).ToList();
        developer.Games = games;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
