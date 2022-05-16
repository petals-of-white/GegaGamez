using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.BLL.Services;
using GegaGamez.Shared.DataAccess;
using Moq;
using Xunit;
namespace GegaGamez.Tests.ServicesTests;

public class GameService_Tests
{
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly GameService _gameService;
    public GameService_Tests()
    {
        _gameService = new(_uowMock.Object);
    }

    [Fact]
    public void Filter_Works()
    {
        Assert.
    }
}
