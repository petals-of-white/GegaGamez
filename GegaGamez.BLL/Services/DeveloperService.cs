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

    public IEnumerable<Developer> GetAll()
    {
        //IEnumerable<Developer> output;

        var developers = _db.Developers.List();

        //output = AutoMapping.Mapper.Map<IEnumerable<Developer>>(developers);

        return developers;
    }

    public Developer? GetById(int id)
    {
        var developer = _db.Developers.Get(id);
        //var developer = AutoMapping.Mapper.Map<Developer>(_db.Developers.Get(id));
        return developer;
    }

    public IEnumerable<Developer> Find(string name)
    {
        //IEnumerable<Developer> output;

        var developersByName = _db.Developers.GetAllByName(name);
        //output = AutoMapping.Mapper.Map<IEnumerable<Developer>>(developersByName);
        return developersByName;
    }

    // Load developer's games
    public void LoadGames(Developer developer)
    {
        var games = _db.Games.GetAll(g => g.DeveloperId == developer.Id);
        //developer.Games = AutoMapping.Mapper.Map<IEnumerable<Game>>(games).ToList();
        developer.Games = games as ICollection<Game>;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
