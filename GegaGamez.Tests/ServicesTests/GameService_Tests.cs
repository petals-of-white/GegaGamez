using System;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.DataAccess;
using Moq;
using Xunit;

namespace GegaGamez.Tests.ServicesTests;

public class GameService_Tests
{
    private readonly GameService _gameService;
    private readonly Mock<IUnitOfWork> _uowMock = new();

    public GameService_Tests()
    {
        _gameService = new(_uowMock.Object);
    }

    [Fact]
    public void Constructor_ShouldThrowNullReferenceException()
    {
        GameService serviceWithNullUoW;
        Assert.Throws<ArgumentNullException>(() => serviceWithNullUoW = new(null!));
    }
}
