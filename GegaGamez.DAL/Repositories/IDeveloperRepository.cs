using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        IEnumerable<Developer> GetActiveDevelopers();

        IEnumerable<Developer> GetAllByName(string name);
    }
}
