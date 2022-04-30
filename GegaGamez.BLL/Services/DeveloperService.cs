using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;

namespace GegaGamez.BLL.Services;

public class DeveloperService : IDisposable
{
    private readonly IUnitOfWork _db;

    public DeveloperService(string connectionString)
    {
        _db = new UnitOfWork(connectionString);
    }

    public IEnumerable<Developer> GetAll()
    {
        IEnumerable<Developer> output;

        var developers = _db.Developers.List();

        output = AutoMapping.Mapper.Map<IEnumerable<Developer>>(developers);

        return output;
    }

    public Developer? GetById(int id)
    {
        var developer = AutoMapping.Mapper.Map<Developer>(_db.Developers.Get(id));

        return developer;
    }

    public IEnumerable<Developer> FindByName(string name)
    {
        IEnumerable<Developer> output;

        var developersByName = _db.Developers.GetAllByName(name);

        output = AutoMapping.Mapper.Map<IEnumerable<Developer>>(developersByName);
        return output;
    }

    // Load developer's games
    public void LoadGames(Developer developer)
    {
        var games = _db.Games.GetAll(g => g.DeveloperId == developer.Id);
        developer.Games = AutoMapping.Mapper.Map<IEnumerable<Game>>(games).ToList();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
