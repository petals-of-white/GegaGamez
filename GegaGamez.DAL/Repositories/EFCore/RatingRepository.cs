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
            throw new NotImplementedException();
        }
    }
}
