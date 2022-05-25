using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Pages.Games;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GegaGamez.Tests.UITests;

public class GameAdd_Tests
{
    private readonly AddModel _addGamePage;
    private readonly Mock<IGameCollectionService> _collectionServiceMock = new();
    private readonly Mock<IDeveloperService> _devServiceMock = new();
    private readonly Mock<IGameService> _gameServiceMock = new();
    private readonly Mock<IGenreService> _genreServiceMock = new();
    private readonly Mock<ILogger<AddModel>> _loggerMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    private HashSet<Game> Games = new() {
        new ()
        {
            Id=1,
            Description="Test",
            Title="Title1",
            DeveloperId=1,
            ReleaseDate = DateTime.Now,
            Genres = new List<Genre>(){new (){ Id = 1} }
        },
        new ()
        {
            Id=2,
            Description="Test",
            Title="Title1",
            DeveloperId=1,
            ReleaseDate = DateTime.Now,
            Genres = new List<Genre>(){new (){ Id = 2} }
        },
    };

    private HashSet<Genre> Genres = new() {
        new ()
        {
            Id=1,
            Description="Test",
            Name="Name",
            Games=new List<Game>(){ new () { Id = 1 } }
        },
        new ()
        {
            Id=2,
            Description="Test2",
            Name="Name2",
            Games=new List<Game>(){ new Game { Id = 2 } }
        }
    };

    public GameAdd_Tests()
    {
        _addGamePage = new(_mapperMock.Object,
                           _devServiceMock.Object,
                           _genreServiceMock.Object,
                           _gameServiceMock.Object,
                           _loggerMock.Object);
    }

    [Fact]
    public void Test()
    {
        _genreServiceMock.Setup(s => s.FindAll()).Returns(Genres).Verifiable();
        _gameServiceMock.Setup(s => s.FindAll()).Returns(Games).Verifiable();

        _addGamePage.OnGet();

        IEnumerable<int>? expected = Genres.Select(g => g.Id);
        IEnumerable<int>? actual = _addGamePage.Genres.Select(g => int.Parse(g.Value));

        Assert.Equal(expected, actual);
        _genreServiceMock.Verify();
        _gameServiceMock.Verify();
    }
}
