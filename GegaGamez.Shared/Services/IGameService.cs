using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGameService : IDisposable
{
    IEnumerable<Game> Find(string? byTitle, params Genre [] byGenre);

    IEnumerable<Game> FindAll();

    Game? GetById(int id);

    IEnumerable<Game> GetDeveloperGames(Developer dev);

    IEnumerable<Game> GetGamesInCollection(UserCollection userCollection);

    IEnumerable<Game> GetGamesInCollection(DefaultCollection defaultCollection);
}
