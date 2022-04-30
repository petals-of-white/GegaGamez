using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class GameRepository : Repository<Game>, IGameRepository
{
    protected override IQueryable<Game> DbSetWithIncludedProperties => _dbSet.Include(g => g.Developer);

    public GameRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public override Game? Get(int id) => DbSetWithIncludedProperties.SingleOrDefault(g => g.Id == id);

    public IEnumerable<Game> GetAllByRatingScore(int score)
    {
        return DbSetWithIncludedProperties.Where(g => Math.Floor(g.Ratings.Average(r => r.RatingScore)) == score).ToList();
    }

    public async Task<IEnumerable<Game>> GetAllByRatingScoreAsync(int score)
    {
        return await DbSetWithIncludedProperties.Where(g => Math.Floor(g.Ratings.Average(r => r.RatingScore)) == score).ToListAsync();
    }

    public IEnumerable<Game> GetAllByTitle(string title)
    {
        var gamesByTitle = (from game in DbSetWithIncludedProperties
                            where game.Title.ToLower().Contains(title.ToLower())
                            select game).ToList();

        return gamesByTitle;
    }

    public async Task<IEnumerable<Game>> GetAllByTitleAsync(string title)
    {
        var gamesByTitle = await (from game in DbSetWithIncludedProperties
                                  where game.Title.ToLower().Contains(title.ToLower())
                                  select game).ToListAsync();

        return gamesByTitle;
    }

    public override Task<Game?> GetAsync(int id) => DbSetWithIncludedProperties.SingleOrDefaultAsync(g => g.Id == id);

    public IEnumerable<Game> GetByGenre(Genre genre)
    {
        var gamesByGenre = (from game in DbSetWithIncludedProperties
                            where game.Genres.Select(g => g.Id).Contains(genre.Id)
                            select game).ToList();
        return gamesByGenre;
    }

    public async Task<IEnumerable<Game>> GetByGenreAsync(Genre genre)
    {
        var gamesByGenre = await (from game in DbSetWithIncludedProperties
                                  where game.Genres.Select(g => g.Id).Contains(genre.Id)
                                  select game).ToListAsync();
        return gamesByGenre;
    }
}
