using System.Linq.Expressions;
using EntityFramework.Exceptions.Common;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.BLL.Services;

public class RatingService : IRatingService, IDisposable
{
    private readonly IUnitOfWork _db;
    private readonly Expression<Func<Rating, object>> [] _ratingIncludes = { r => r.User, r => r.Game };

    public RatingService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void Dispose() => _db.Dispose();

    public byte? GetAverageRatingScore(Game game) => _db.Ratings.GetAvgRating(game);

    public Rating? GetUserRating(User user, Game game) =>
        _db.Ratings.FindAll(r => r.UserId == user.Id && r.GameId == game.Id, _ratingIncludes)
        .SingleOrDefault();

    /// <summary>
    /// </summary>
    /// <param name="rating"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="UniqueEntityException"></exception>
    public void RateGame(Rating rating)
    {
        var actualGame = _db.Games.Get(rating.GameId)
            ?? throw new EntityNotFoundException(rating, null);
        var actualUser = _db.Users.Get(rating.UserId)
            ?? throw new EntityNotFoundException(rating, null);

        Rating? actualRating = _db.Ratings.FindAll(r => r.UserId == rating.UserId && r.GameId == rating.GameId)
            .SingleOrDefault();

        if (actualRating is null)
            _db.Ratings.Add(rating);
        else
        {
            actualRating.RatingScore = rating.RatingScore;
            _db.Update(actualRating);
        }

        try
        {
            _db.Save();
        }
        catch (UniqueConstraintException ex)
        {
            throw new UniqueEntityException(rating, ex);
        }
    }

    public void UnrateGame(Rating rating)
    {
        try
        {
            _db.Ratings.Remove(rating);
            _db.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityNotFoundException(rating, ex);
        }
    }
}
