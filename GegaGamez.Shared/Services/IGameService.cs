using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGameService : IDisposable
{
    void CreateGame(Game game);

    void DeleteGame(Game game);

    IEnumerable<Game> Find(string? byTitle, params Genre [] byGenre);

    IEnumerable<Game> FindAll();

    Game? GetById(int id);

    IEnumerable<Game> GetDeveloperGames(Developer dev);

    IEnumerable<Game> GetGamesInCollection(UserCollection userCollection);

    IEnumerable<Game> GetGamesInCollection(DefaultCollection defaultCollection);

    int GetNumberOfGamesForDeveloper(Developer dev);

    void UpdateGame(Game game);
}
