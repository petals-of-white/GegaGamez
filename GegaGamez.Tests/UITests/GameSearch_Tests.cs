using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;
using GegaGamez.WebUI.Models.Display;
using GegaGamez.WebUI.Pages.Games;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GegaGamez.Tests.UITests;

public class GameSearch_Tests
{
    private readonly Mock<IGameService> _gameServiceMock = new();
    private readonly Mock<IGenreService> _genreServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IRatingService> _ratingServiceMock = new();
    private readonly SearchModel _searchPage;
    private readonly Mock<ILogger<SearchModel>> _loggerMock = new();

    private List<Game> Games { get; } = new() {
        new ()
        {
            Id = 1,
            Title = "Test",
            ReleaseDate = DateTime.Today.AddYears(-2),
            Description = "Descr",
        },

        new ()
        {
            Id = 2,
            Title = "Test2",
            ReleaseDate = DateTime.Today.AddYears(-3),
            Description = "Descr"
        }
        };

    private List<Genre> Genres { get; } = new();

    public GameSearch_Tests()
    {
        _searchPage = new(_gameServiceMock.Object,
                          _genreServiceMock.Object,
                          _mapperMock.Object,
                          _ratingServiceMock.Object,
                          _loggerMock.Object);
        

        _genreServiceMock.Setup(service => service.GetGameGenres(It.IsNotNull<Game>()))
    .Verifiable();

        _mapperMock.Setup(m => m.Map<List<GenreModel>>(It.IsAny<IEnumerable<Genre>>()))
    .Returns<IEnumerable<Genre>>((genreCol) =>
    {
        var list = new List<GenreModel>();
        foreach (var genre in genreCol)
        {
            list.Add(new GenreModel
            {
                Id = genre.Id,
                Description = genre.Description,
                Name = genre.Name
            });
        }
        return list;
    });

        _mapperMock.Setup(m => m.Map<List<GameModel>>(It.IsNotNull<object>()))
    .Returns(new List<GameModel>() {
                new ()
                {
                    Id = 1,
                    Description ="Something",
                    Title = "Hello"
                },

                new ()
                {
                    Id = 2,
                    Description ="Something2",
                    Title = "Hello2"
                },
    });
    }

    [Theory]
    [InlineData(null, 1, 2, 3)]
    [InlineData("", 3, 1)]
    [InlineData("Hello")]
    public void Search_FiltersGamesByGenresAndTitle_IfSpecified(string? title, params int [] byGenre)
    {
        // Arrange
        _gameServiceMock.Setup(service => service.Find(It.IsAny<string?>(), It.IsAny<Genre []>()))
            .Returns(Games).Verifiable();

        // Act
        _searchPage.ByGenre = byGenre.ToHashSet();
        _searchPage.GameTitle = title;
        _searchPage.OnGet();

        // Assert
        _gameServiceMock.Verify();
        _genreServiceMock.Verify();
        Assert.Equal(Games.Select(g => g.Id), _searchPage.Games.Select(g => g.Id));
    }

    [Fact]
    public void Search_ReturnAllGames_IfQueryEmpty()
    {
        // Arrange
        _gameServiceMock.Setup(service => service.FindAll())
            .Returns(Games).Verifiable();

        _mapperMock.Setup(m => m.Map<List<GenreModel>>(It.IsAny<IEnumerable<Genre>>()))
            .Returns<IEnumerable<Genre>>((genreCol) =>
            {
                var list = new List<GenreModel>();
                foreach (var genre in genreCol)
                {
                    list.Add(new GenreModel
                    {
                        Id = genre.Id,
                        Description = genre.Description,
                        Name = genre.Name
                    });
                }
                return list;
            });

        _mapperMock.Setup(m => m.Map<List<GameModel>>(It.IsNotNull<object>()))
            .Returns(new List<GameModel>() {
                new ()
                {
                    Id = 1,
                    Description ="Something",
                    Title = "Hello"
                },

                new ()
                {
                    Id = 2,
                    Description ="Something2",
                    Title = "Hello2"
                },
            });

        // Act
        _searchPage.OnGet();

        // Assert
        _gameServiceMock.Verify();
        _genreServiceMock.Verify();
        Assert.Equal(Games.Select(g => g.Id), _searchPage.Games.Select(g => g.Id));
    }
}
