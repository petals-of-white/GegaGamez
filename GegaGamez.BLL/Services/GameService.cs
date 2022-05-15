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

    public void Dispose()
    {
        _db.Dispose();
    }
}
