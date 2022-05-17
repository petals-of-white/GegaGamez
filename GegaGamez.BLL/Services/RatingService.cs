using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class RatingService : IRatingService, IDisposable
{
    private readonly IUnitOfWork _db;

    public RatingService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Dispose() => _db.Dispose();

    public byte GetAverageRatingScore(Game game)
    {
        throw new NotImplementedException();
    }

    public Rating? GetUserRating(User user, Game game) =>
        _db.Ratings.FindAll(r => r.UserId == user.Id && r.GameId == game.Id)
        .SingleOrDefault();

    /*
    public void RateGame(Rating rating)
    {
        Rating? possibleRating = _db.Ratings.FindAll(r => r.UserId == rating.UserId && r.GameId == rating.GameId)
            .SingleOrDefault();

        if (possibleRating is not null)
        {
            possibleRating.RatingScore = rating.RatingScore;
            _db.Update(possibleRating);
        }
        else
            _db.Ratings.Add(rating);

        _db.Save();
    }
    */

    public void RateGame(Rating rating)
    {
        _db.Update(rating);

        _db.Save();
    }

    public void UnrateGame(Rating rating)
    {
        _db.Ratings.Remove(rating);
        _db.Save();
    }
}
