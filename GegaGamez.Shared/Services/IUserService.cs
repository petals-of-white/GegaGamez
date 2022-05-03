using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IUserService
{
    void Create(User newUser);

    void AddComment(Comment comment);

    void AddGameToCollection(DefaultCollection defaultCollection, Game game);

    void AddGameToCollection(UserCollection userCollection, Game game);

    void CreateUserCollection(UserCollection newCollection);

    void DeleteCollection(UserCollection userCollection);

    IEnumerable<User> FindByUsername(string username);

    IEnumerable<User> GetAll();

    User? GetById(int id);

    User? GetByUsername(string username);

    Rating? GetRatingForGame(User user, Game game);

    void LoadUsersCollections(User user);

    void RateGame(Rating rating);

    void RemoveGameFromCollection(DefaultCollection defaultCollection, Game game);

    void RemoveGameFromCollection(UserCollection userCollection, Game game);

    void Unrate(Rating rating);

    User UpdateUser(User user);
}
