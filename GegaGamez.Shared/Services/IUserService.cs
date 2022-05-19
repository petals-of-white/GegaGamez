using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IUserService
{
    void CreateUser(User newUser);

    void DeleteUser(User user);

    IEnumerable<User> Find(string username);

    IEnumerable<User> GetAll();

    User? GetById(int id);

    User? GetByUsername(string username);

    void UpdateUser(User user);
}
