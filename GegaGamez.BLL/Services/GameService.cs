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
        _db = db;
    }

    public IEnumerable<Game> GetAll()
    {
        //ICollection<Game> output = new List<Game>();

        //output = AutoMapping.Mapper.Map<IEnumerable<Game>>(games);

        /*
        foreach (var gameEntity in games)
        {
            //var game = AutoMapping.Mapper.Map<Game>(gameEntity);
            game.AvgRatingScore = (byte) _db.Ratings.GetAverageGameRatingScore(gameEntity);
            output.Add(game);
        }
        */
        var games = _db.Games.List();
        return games;
    }

    public Game? GetById(int id)
    {
        //var gameEntity = _db.Games.Get(id);

        //var game = AutoMapping.Mapper.Map<Game>(gameEntity);

        // do the avg for score !!!
        //if (gameEntity is not null)
        //{
        //    game.AvgRatingScore = (byte) _db.Ratings.GetAverageGameRatingScore(gameEntity);
        //}

        var game = _db.Games.Get(id);
        return game;
    }

    public IEnumerable<Game> FindByTitle(string title)
    {
        //ICollection<Game> output = new List<Game>();
        var gameEntitiesByTitle = _db.Games.GetAllByTitle(title);

        /*
        foreach (var gameEntity in gameEntitiesByTitle)
        {
            //var gameByTitle = AutoMapping.Mapper.Map<Game>(gameEntity);
            //gameEntity.AvgRatingScore = (byte) _db.Ratings.GetAverageGameRatingScore(gameEntity);

            //output.Add(gameByTitle);
        }
        */

        return gameEntitiesByTitle;
    }

    public IEnumerable<Game> GetByGenre(Genre genre)
    {
        //IEnumerable<Game> output;
        //var gamesByGenre = _db.Games.GetByGenre(AutoMapping.Mapper.Map<Shared.Entities.Genre>(genre));
        var gamesByGenre = _db.Games.GetByGenre(genre);
        //output = AutoMapping.Mapper.Map<IEnumerable<Game>>(gamesByGenre);

        return gamesByGenre;
    }

    public void LoadGameComments(Game game)
    {
        //IEnumerable<Comment> comments = AutoMapping.Mapper
        //.Map<IEnumerable<Comment>>(_db.Comments.GetAll(c => c.GameId == game.Id));
        var comments = _db.Comments.GetAll(c => c.GameId == game.Id);
        game.Comments = comments as ICollection<Comment>;
    }

    public void LoadGameGenres(Game game)
    {
        //var genres = _db.Genres.GetGamesGenres(AutoMapping.Mapper.Map<Shared.Entities.Game>(game));
        //game.Genres = AutoMapping.Mapper.Map<IEnumerable<Genre>>(genres).ToList();

        var genres = _db.Genres.GetGamesGenres(game);
        game.Genres = genres as ICollection<Genre>;
    }

    public IEnumerable<Game> Find(string? byTitle, params Genre [] byGenre)
    {
        Expression<Func<Game, bool>> filter;

        IEnumerable<Game> filteredGames;

        var genresIds = byGenre.Select(genre => genre.Id);

        if (string.IsNullOrWhiteSpace(byTitle))
        {
            if (byGenre is null || byGenre.Length == 0)
            {
                filter = g => true;
            }
            else
            {
                filter = g => g.Genres.Select(genre => genre.Id).ToHashSet().IsSupersetOf(genresIds);
            }
        }
        else
        {
            filter =
                g => g.Genres.Select(genre => genre.Id).ToHashSet().IsSupersetOf(genresIds)
                && g.Title.ToLower().Contains(byTitle.ToLower());
        }

        filteredGames = _db.Games.GetAll(filter);

        return filteredGames;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
