using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class RatingService : IRatingService, IDisposable
{
    private readonly IUnitOfWork _db;
    private Expression<Func<Rating, object>> [] _ratingIncludes = {r=>r.User, r=>r.Game};

    public RatingService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Dispose() => _db.Dispose();

    public byte? GetAverageRatingScore(Game game) => _db.Ratings.GetAvgRating(game);

    public Rating? GetUserRating(User user, Game game) =>
        _db.Ratings.FindAll(r => r.UserId == user.Id && r.GameId == game.Id, _ratingIncludes)
        .SingleOrDefault();

    public void RateGame(Rating rating)
    {
        Rating? actualRating = _db.Ratings.FindAll(r => r.UserId == rating.UserId && r.GameId == rating.GameId)
            .SingleOrDefault();

        if (actualRating is null)
            _db.Ratings.Add(rating);
        else
        {
            actualRating.RatingScore = rating.RatingScore;
            _db.Update(actualRating);
        }

        _db.Save();
    }

    public void UnrateGame(Rating rating)
    {
        _db.Ratings.Remove(rating);
        _db.Save();
    }
}
