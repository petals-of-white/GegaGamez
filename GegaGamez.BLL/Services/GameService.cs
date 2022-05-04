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

    public IEnumerable<Game> FindAll() => _db.Games.AsEnumerable();

    public Game? GetById(int id) => _db.Games.Get(id);

    public void LoadGameComments(Game game)
    {
        var comments = _db.Comments.FindAll(c => c.GameId == game.Id);
        game.Comments = comments as ICollection<Comment>;
    }

    public void LoadGameGenres(Game game)
    {
        var genres = _db.Genres.FindAll(genre => genre.Games.Select(g => g.Id).Contains(game.Id));
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

        filteredGames = _db.Games.FindAll(filter);

        return filteredGames;
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
