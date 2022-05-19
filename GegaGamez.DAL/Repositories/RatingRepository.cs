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

    // Need to think about this one and async
    /*
    public override Rating? Get(int id)
    {
        var explanation = "This method should not be called by rating repository, " +
            "because Rating entity doesn't have an Id property";

        throw new NotImplementedException(explanation);
    }
    */

    public byte GetAllGamesAvgRating()
    {
        if (DbSetWithIncludedProperties.Count() == 0)
            return 0;
        else
            return (byte) Math.Round(DbSetWithIncludedProperties.Average(r => r.RatingScore));
    }

    /*
    public override Task<Rating?> GetAsync(int id)
    {
        var explanation = "This method should not be called by rating repository, " +
            "because Rating entity doesn't have an Id property";

        throw new NotImplementedException(explanation);
    }
    */

    public byte? GetAvgRating(Game game)
    {
        if (DbSetWithIncludedProperties.Where(r => r.GameId == game.Id).Count() == 0)
            return null;
        else
            return (byte) Math.Round(DbSetWithIncludedProperties.Where(r => r.GameId == game.Id)
                .Average(r => r.RatingScore));
    }
}
