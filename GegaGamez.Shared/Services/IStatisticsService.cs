namespace GegaGamez.Shared.Services;

public interface IStatisticsService
{
    int AdminsQuantity { get; }
    public byte AvgGameScore { get; }
    int CommentsQuantity { get; }
    int DevsQuantity { get; }
    int GamesQuantity { get; }
    int GenresQuantity { get; }
    int UsersQuantity { get; }
}
