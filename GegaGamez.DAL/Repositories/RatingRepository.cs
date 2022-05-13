using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class RatingRepository : Repository<Rating>, IRatingRepository
{
    protected override IQueryable<Rating> DbSetWithIncludedProperties => _dbSet.Include(r => r.User).Include(r => r.Game);

    public RatingRepository(DbContext dbContext) : base(dbContext)
    {
    }

    // Need to think about these two
    public override Rating? Get(int id)
    {
        var explanation = "This method should not be called by rating repository, " +
            "because Rating entity doesn't have an Id property";

        throw new NotImplementedException(explanation);
    }

    public override Task<Rating?> GetAsync(int id)
    {
        var explanation = "This method should not be called by rating repository, " +
            "because Rating entity doesn't have an Id property";

        throw new NotImplementedException(explanation);
    }

    public int GetAverageGameRatingScore(Game game)
    {
        return (int) Math.Floor(DbSetWithIncludedProperties.Where(r => r.GameId == game.Id).Average(r => r.RatingScore));
    }

    public async Task<int> GetAverageGameRatingScoreAsync(Game game)
    {
        var avg = await DbSetWithIncludedProperties.Where(r => r.GameId == game.Id).AverageAsync(r => r.RatingScore);

        return (int) Math.Floor(avg);
    }

    public Rating? GetUserRatingForAGame(User user, Game game)
    {
        return DbSetWithIncludedProperties.SingleOrDefault(r => r.UserId == user.Id && r.GameId == game.Id);
    }

    public Task<Rating?> GetUserRatingForAGameAsync(User user, Game game)
    {
        return DbSetWithIncludedProperties.SingleOrDefaultAsync(r => r.UserId == user.Id && r.GameId == game.Id);
    }

    public byte GetAllGamesAvgRating() =>
        (byte) Math.Round(DbSetWithIncludedProperties.Average(r => r.RatingScore));
}
