using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGameService : IDisposable
{
    IEnumerable<Game> Find(string? byTitle, params Genre [] byGenre);

    IEnumerable<Game> FindAll();

    Game? GetById(int id);

    void DeleteGame(Game game);

    IEnumerable<Game> GetDeveloperGames(Developer dev);

    IEnumerable<Game> GetGamesInCollection(UserCollection userCollection);

    IEnumerable<Game> GetGamesInCollection(DefaultCollection defaultCollection);
    void CreateGame(Game game);

    void UpdateGame(Game game);

    int GetNumberOfGamesForDeveloper(Developer dev);
}
