using GegaGamez.DAL.Entities;

namespace GegaGamez.DAL.Repositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        /// <summary>
        /// </summary>
        /// <param name="game"></param>
        /// <returns>Int number (1-10) that describes average rating for a game</returns>
        int GetAverageGameRatingScore(Game game);
    }
}
