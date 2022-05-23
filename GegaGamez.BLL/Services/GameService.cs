using System.Linq.Expressions;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Entities;
using GegaGamez.Shared.Exceptions;
using GegaGamez.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.BLL.Services;

public class GameService : IDisposable, IGameService
{
    private readonly IUnitOfWork _db;
    private readonly Expression<Func<Game, object>> [] _gameIncludes = { g => g.Developer, g => g.Genres };

    private List<Genre> LoadActualGenres(List<Genre> genres)
    {
        List<Genre> actualGenres = new(genres.Count);

        for (int i = 0; i < genres.Count; i++)
        {
            var actualGenre = _db.Genres.Get(genres [i].Id);
            if (actualGenre is not null) actualGenres.Add(actualGenre);
        }

        return actualGenres;
    }

    public GameService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="game"></param>
    /// <exception cref="UniqueEntityException"></exception>
    public void CreateGame(Game game)
    {
        game.Genres = LoadActualGenres(game.Genres.ToList());
        try
        {
            _db.Games.Add(game);
            _db.Save();
        }
        catch (EntityFramework.Exceptions.Common.UniqueConstraintException ex)
        {
            throw new UniqueEntityException(game, ex);
        }
    }

    public void DeleteGame(Game game)
    {
        try
        {
            _db.Games.Remove(game);
            _db.Save();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new EntityNotFoundException(game, ex);
        }
    }

    public void Dispose() => _db.Dispose();

    public IEnumerable<Game> Find(string? byTitle, params Genre [] byGenre)
    {
        Expression<Func<Game, bool>> filter = g => true;

        if (string.IsNullOrWhiteSpace(byTitle) == false)
            filter = filter.AndAlso(g => g.Title.ToLower().Contains(byTitle.ToLower()));

        if (byGenre is not null && byGenre.Length != 0)
        {
            var filterGenreIds = byGenre.Select(genre => genre.Id);
            foreach (var filterGenreId in filterGenreIds)
            {
                filter = filter.AndAlso(g => g.Genres.Select(g => g.Id).Contains(filterGenreId));
            }
            //filter = filter.AndAlso(g => filterGenreIds.All(gid => g.Genres.Select(gr => gr.Id).Contains(gid)));
        }
        return _db.Games.FindAll(filter, _gameIncludes);
    }

    /*
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
    */

    public IEnumerable<Game> FindAll() => _db.Games.AsEnumerable(_gameIncludes);

    public Game? GetById(int id) => _db.Games.Get(id, _gameIncludes);

    public IEnumerable<Game> GetDeveloperGames(Developer dev) =>
        _db.Games.FindAll(g => g.DeveloperId == dev.Id, _gameIncludes);

    public IEnumerable<Game> GetGamesInCollection(UserCollection userCollection) =>
            _db.Games.FindAll(g => g.UserCollections.Select(uc => uc.Id).Contains(userCollection.Id), _gameIncludes);

    public IEnumerable<Game> GetGamesInCollection(DefaultCollection defaultCollection) =>
        _db.Games.FindAll(g => g.DefaultCollections.Select(dc => dc.Id).Contains(defaultCollection.Id), _gameIncludes);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="game"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    /// <exception cref="UniqueEntityException"></exception>
    public void UpdateGame(Game game)
    {
        var actualGame = _db.Games.Get(game.Id, _gameIncludes.Append(g=>g.Genres).ToArray())
            ?? throw new EntityNotFoundException(game, null);

        actualGame.Description = game.Description;
        actualGame.Title = game.Title;
        actualGame.DeveloperId = game.DeveloperId;
        actualGame.ReleaseDate = game.ReleaseDate;

        actualGame.Genres = new HashSet<Genre>();

        try
        {
            _db.Save();

            actualGame.Genres = LoadActualGenres(game.Genres.ToList());

            _db.Save();
        }
        catch (EntityFramework.Exceptions.Common.UniqueConstraintException ex)
        {
            throw new UniqueEntityException("Can not insert duplicate genres.",ex);
        }
    }
}
