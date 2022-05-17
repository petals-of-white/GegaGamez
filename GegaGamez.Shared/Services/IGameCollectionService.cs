using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGameCollectionService
{
    void AddGame(DefaultCollection defaultCollection, Game game);

    void AddGame(UserCollection userCollection, Game game);

    void CreateUserCollection(UserCollection newCollection);

    void DeleteCollection(UserCollection userCollection);

    DefaultCollection? GetDefaultCollectionById(int id);

    IEnumerable<DefaultCollectionType> GetDefaultCollectionTypes();

    IEnumerable<DefaultCollection> GetDefaultColletionsForUser(User user);

    UserCollection? GetUserCollectionById(int id);

    IEnumerable<UserCollection> GetUserCollectionsForUser(User user);

    void RemoveGame(DefaultCollection defaultCollection, Game game);

    void RemoveGame(UserCollection userCollection, Game game);
}
