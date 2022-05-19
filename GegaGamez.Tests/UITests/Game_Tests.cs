using AutoMapper;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Pages.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GegaGamez.Tests.UITests;

public class Game_Tests
{
    private readonly Mock<IAuthorizationService> _authServiceMock = new();
    private readonly Mock<IGameCollectionService> _collectionServiceMock = new();
    private readonly Mock<ICommentService> _commentService = new();
    private readonly Mock<ICountryService> _countryServiceMock = new();
    private readonly IndexModel _gamePage;
    private readonly Mock<IGameService> _gameService = new();
    private readonly Mock<IGenreService> _genreService = new();
    private readonly Mock<ILogger<IndexModel>> _loggerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IRatingService> _ratingService = new();
    private readonly Mock<IUserService> _userServiceMock = new();

    public Game_Tests()
    {
        _gamePage = new(
            _gameService.Object,
            _collectionServiceMock.Object,
            _ratingService.Object,
            _commentService.Object,
            _userServiceMock.Object,
            _genreService.Object,
            _mapperMock.Object,
            _authServiceMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public void Smth()
    {
    }
}
