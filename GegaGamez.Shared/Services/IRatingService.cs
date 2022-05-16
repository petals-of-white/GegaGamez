using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IRatingService
{
    byte GetAverageRatingScore(Game game);

    Rating? GetUserRating(User user, Game game);

    void RateGame(Rating rating);

    void UnrateGame(Rating rating);
}
