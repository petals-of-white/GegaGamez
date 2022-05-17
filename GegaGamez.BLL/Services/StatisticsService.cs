using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Services;

namespace GegaGamez.BLL.Services;

public class StatisticsService : IStatisticsService, IDisposable
{
    private readonly IUnitOfWork _db;

    public StatisticsService(IUnitOfWork db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db), "db cannot be null");
    }

    public int AdminsQuantity => _db.Users.CountAll(u => u.Roles.Select(r => r.Name).Contains("Admin"));
    public byte AvgGameScore => _db.Ratings.GetAllGamesAvgRating();
    public int CommentsQuantity => _db.Comments.Count();
    public int DevsQuantity => _db.Developers.Count();
    public int GamesQuantity => _db.Games.Count();
    public int GenresQuantity => _db.Genres.Count();
    public int UsersQuantity => _db.Users.Count();

    public void Dispose() => _db.Dispose();
}
