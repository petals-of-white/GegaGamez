using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.DataAccess.Repositories
{
    public interface IDeveloperRepository : IRepository<Developer>
    {
        IEnumerable<Developer> GetActiveDevelopers();

        Task<IEnumerable<Developer>> GetActiveDevelopersAsync();

        IEnumerable<Developer> GetAllByName(string name);

        Task<IEnumerable<Developer>> GetAllByNameAsync(string name);
    }
}
