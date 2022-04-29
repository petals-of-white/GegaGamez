using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        IEnumerable<Genre> GetAllByName(string name);

        Task<IEnumerable<Genre>> GetAllByNameAsync(string name);

        IEnumerable<Genre> GetGamesGenres(Game game);

        Task<IEnumerable<Genre>> GetGamesGenresAsync(Game game);
    }
}
