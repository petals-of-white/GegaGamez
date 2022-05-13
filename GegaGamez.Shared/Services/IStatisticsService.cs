namespace GegaGamez.Shared.Services;

public interface IStatisticsService
{
    int GamesQuantity { get; }
    int DevsQuantity { get; }
    int UsersQuantity { get; }
    int GenresQuantity { get; }
    int AdminsQuantity { get; }
    int CommentsQuantity { get; }
    public byte AvgGameScore { get; }
}
