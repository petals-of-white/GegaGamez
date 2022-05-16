using System;
using System.Linq;
using GegaGamez.DAL;
using GegaGamez.DAL.Data;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace GegaGamez.Tests.DALTests;

public class UnitOfWork_Tests : IDisposable
{
    private readonly UnitOfWork _db;
    private readonly GegaGamezContext _dbContext;
    private readonly Game [] _games;
    private readonly ITestOutputHelper _output;
    private readonly Rating [] _ratings;
    private readonly User [] _users;

    public UnitOfWork_Tests(ITestOutputHelper outputHelper)
    {
        _output = outputHelper;

        var contextOptions = new DbContextOptionsBuilder<GegaGamezContext>()
            .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GegaGamez;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            .Options;

        _dbContext = new(contextOptions);
        _db = new(_dbContext);

        /*
        var contextOptions = new DbContextOptionsBuilder<GegaGamezContext>().UseInMemoryDatabase("TestDb").Options;
        _dbContext = new(contextOptions);

        _db = new(_dbContext);
        _users = new User [] {
            new()
            {
                Username="Rerorer",
                Password="SpookyScary"
            },

            new()
            {
                Username="BigBlackCat",
                Password="SoNeedlessToSay"
            },

            new()
            {
                Username="ZombieO",
                Password="Yuyuyu"
            }
        };
        _db.Users.AddRange(_users);

        _games = new Game []
        {
            new()
            {
                Title="TestGameOne",
                ReleaseDate= DateTime.Today,
                Description="TastyTest",
                DeveloperId=3
            },
            new()
            {
                Title="TestGameTwo",
                ReleaseDate= DateTime.Today.AddYears(-1),
                Description="TestyTasty",
                DeveloperId=5
            },
            new()
            {
                Title="TestGameThree",
                ReleaseDate= DateTime.Today.AddYears(-2),
                Description="West?",
                DeveloperId=1
            },
        };
        _db.Games.AddRange(_games);

        _ratings = new Rating []
        {
            new () { User = _users[0], Game = _games[0], RatingScore = 5},
            new () { User = _users[1], Game = _games[1], RatingScore = 8},
            new () { User = _users[2], Game = _games[2], RatingScore = 10},
        };

        _db.Ratings.AddRange(_ratings);
        _db.Save();
        */
        _output = outputHelper;
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void AddingDublicate_ThrowsException(int gameIndex, int userIndex)
    {
        var dublicateRating = new Rating { UserId = _users [userIndex].Id, GameId = _games [gameIndex].Id, RatingScore = 2 };
        Assert.Throws<DatabaseException>(() =>
        {
            _db.Ratings.Add(dublicateRating);
            _db.Save();
            _output.WriteLine("Dublicate has been added.. huh..");
        });
    }

    [Fact]
    public void AddingDublicateToSqlDb_ThrowsException()
    {
        var dublicateRating = new Rating { UserId = 1005, GameId = 1005, RatingScore = 2 };
        Assert.Throws<DatabaseException>(() =>
        {
            _db.Ratings.Add(dublicateRating);
            _db.Save();
            _output.WriteLine("Dublicate has been added.. huh..");
        });
    }


    public void Dispose() => _db.Dispose();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    public void Rating_FindsCorrect(int gameIndex, int userIndex)
    {
        var ratingsExpected = _ratings.Where(r => r.User == _users [userIndex] && r.Game == _games [gameIndex]).ToArray();

        var ratingsActual = _db.Ratings.FindAll(r => r.UserId == _users [userIndex].Id && r.GameId == _games [gameIndex].Id).ToArray();

        Assert.Single(ratingsExpected);
        Assert.Single(ratingsActual);
        Assert.Equal(ratingsExpected.Single(), ratingsActual.Single());
    }
}
