using GegaGamez.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Repositories.EFCore
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public int GetAverageGameRatingScore(Game game)
        {
            return (int) Math.Floor(_dbSet.Where(r => r.GameId == game.Id).Average(r => r.RatingScore));
        }

        public async Task<int> GetAverageGameRatingScoreAsync(Game game)
        {
            var avg = await _dbSet.Where(r => r.GameId == game.Id).AverageAsync(r => r.RatingScore);
            
            return (int) Math.Floor(avg);
        }
    }
}