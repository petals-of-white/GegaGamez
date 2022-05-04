using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services
{
    public interface IGameCollectionService
    {
        UserCollection? GetUserCollectionById(int id);

        DefaultCollection? GetDefaultCollectionById(int id);

        IEnumerable<DefaultCollectionType> GetDefaultCollectionTypes();

        void LoadCollectionGames(DefaultCollection defaultCollection);

        void LoadCollectionGames(UserCollection userCollection);
    }
}
