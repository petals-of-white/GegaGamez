using System;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace GegaGamez.Tests.ServicesTests;

public class RatingService_Tests : IDisposable
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly RatingService _ratingService;
    private readonly Mock<IUnitOfWork> _uowMock = new();
    public RatingService_Tests(ITestOutputHelper outputHelper)
    {
        _ratingService = new(_uowMock.Object);
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Constructor_ShouldThrowNullReferenceException()
    {
        RatingService serviceWithNullUoW;
        Assert.Throws<ArgumentNullException>(() => serviceWithNullUoW = new(null));
    }

    [Fact]
    public void AddingNewRating_ActuallyUpdatesThePrevious()
    {
        // Arrange

        var updatedRating = new Rating
        {
            Game = new() { Id = It.IsInRange(1, int.MaxValue, Moq.Range.Inclusive) },
            User = new() { Id = It.IsInRange(1, int.MaxValue, Moq.Range.Inclusive) },
            RatingScore = It.IsInRange((byte) 1, (byte) 10, Moq.Range.Inclusive)
        };

        //_uowMock.Setup(uow => uow.Ratings.FindAll(r => r.UserId == oldRating.User.Id && r.GameId == oldRating.Game.Id))
        //        .Returns(new Rating [] { new() { GameId = oldRating.GameId, UserId = oldRating.UserId } })
        //        .Verifiable();

        _uowMock.Setup(uow => uow.Update<Rating>(updatedRating)).Callback(() =>
        {
            _outputHelper.WriteLine("Updating entity called");
        });

        _uowMock.Setup(uow => uow.Save()).Callback(() =>
        {
            _outputHelper.WriteLine("Saved to the db!!!");
        });

        // Act
        _ratingService.RateGame(updatedRating);
        Rating? actualRating = _ratingService.GetUserRating(updatedRating.User, updatedRating.Game);

        // Assert
        Assert.Equal(updatedRating, actualRating);
    }

    public void Dispose() => _ratingService.Dispose();
}
