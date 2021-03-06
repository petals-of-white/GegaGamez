using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.DataAccess.Repositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        byte GetAllGamesAvgRating();

        /// <summary>
        /// </summary>
        /// <param name="game"></param>
        /// <returns>Int number (1-10) that describes average rating for a game</returns>
        byte? GetAvgRating(Game game);
    }
}
