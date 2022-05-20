using System.Diagnostics;
using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class GameService : IDisposable, IGameService
{
    private readonly IUnitOfWork _db;

    public GameService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public void DeleteGame(Game game)
    {
        _db.Games.Remove(game);
        _db.Save();
    }

    public void CreateGame(Game game)
    {
        _db.Games.Add(game);
        _db.Save();
    }
    public void Dispose() => _db.Dispose();

    public IEnumerable<Game> Find(string? byTitle, params Genre [] byGenre)
    {
        Expression<Func<Game, bool>> filter;

        IEnumerable<Game> filteredGames;

        var genresIds = byGenre.Select(genre => genre.Id);

        if (string.IsNullOrWhiteSpace(byTitle))
        {
            if (byGenre is null || byGenre.Length == 0)
                filter = g => true;
            else
                filter = g => g.Genres.Select(genre => genre.Id).All(id => genresIds.Contains(id));
        }
        else
        {
            if (byGenre is null || byGenre.Length == 0)
                filter = g => g.Title.ToLower().Contains(byTitle.ToLower());
            else
            {
                filter = g => g.Title.ToLower().Contains(byTitle.ToLower())
                    && g.Genres.Select(genre => genre.Id).All(id => genresIds.Contains(id));
            }
        }

        filteredGames = _db.Games.FindAll(filter);

        return filteredGames;
    }

    public IEnumerable<Game> FindAll() => _db.Games.AsEnumerable();

    public Game? GetById(int id) => _db.Games.Get(id);

    public IEnumerable<Game> GetDeveloperGames(Developer dev) =>
        _db.Games.FindAll(g => g.DeveloperId == dev.Id);

    public IEnumerable<Game> GetGamesInCollection(UserCollection userCollection) =>
            _db.Games.FindAll(g => g.UserCollections.Select(uc => uc.Id).Contains(userCollection.Id));

    public IEnumerable<Game> GetGamesInCollection(DefaultCollection defaultCollection) =>
        _db.Games.FindAll(g => g.DefaultCollections.Select(dc => dc.Id).Contains(defaultCollection.Id));

    public void UpdateGame(Game game)
    {
        var actualGame = _db.Games.Get(game.Id);

        if (actualGame is null)
            throw new KeyNotFoundException($"Game with id {game.Id} wasn't found");
        else
        {
            actualGame.Description = game.Description;
            actualGame.Title = game.Title;
            actualGame.DeveloperId = game.DeveloperId;
            actualGame.ReleaseDate = game.ReleaseDate;
            actualGame.Genres = game.Genres;

            _db.Update(actualGame);

            _db.Save();
        }

        
    }
}
