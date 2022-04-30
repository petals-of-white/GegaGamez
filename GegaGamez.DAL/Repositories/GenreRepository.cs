using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public IEnumerable<Genre> GetAllByName(string name)
    {
        var genresByName = (from genre in DbSetWithIncludedProperties
                            where genre.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                            select genre).ToList();
        return genresByName;
    }

    public async Task<IEnumerable<Genre>> GetAllByNameAsync(string name)
    {
        var genresByName = await (from genre in DbSetWithIncludedProperties
                                  where genre.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
                                  select genre).ToListAsync();

        return genresByName;
    }

    public IEnumerable<Genre> GetGamesGenres(Game game)
    {
        var gamesGenres = (from genre in DbSetWithIncludedProperties
                           where genre.Games.Select(game => game.Id).Contains(game.Id)
                           select genre).ToList();
        return gamesGenres;
    }

    public async Task<IEnumerable<Genre>> GetGamesGenresAsync(Game game)
    {
        var gamesGenres = await (from genre in DbSetWithIncludedProperties
                                 where genre.Games.Select(game => game.Id).Contains(game.Id)
                                 select genre).ToListAsync();
        return gamesGenres;
    }
}
