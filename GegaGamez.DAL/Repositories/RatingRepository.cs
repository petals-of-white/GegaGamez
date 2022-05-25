using GegaGamez.Shared.DataAccess.Repositories;
using GegaGamez.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories;

public class RatingRepository : Repository<Rating>, IRatingRepository
{
    public RatingRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public byte GetAllGamesAvgRating()
    {
        if (_dbSet.Count() == 0)
            return 0;
        else
            return (byte) Math.Round(_dbSet.Average(r => r.RatingScore));
    }

    public byte? GetAvgRating(Game game)
    {
        if (_dbSet.Where(r => r.GameId == game.Id).Count() == 0)
            return null;
        else
            return (byte) Math.Round(_dbSet.Where(r => r.GameId == game.Id)
                .Average(r => r.RatingScore));
    }
}
