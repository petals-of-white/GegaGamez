using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        IEnumerable<Genre> GetAllByName(string name);

        Task<IEnumerable<Genre>> GetAllByNameAsync(string name);
    }
}