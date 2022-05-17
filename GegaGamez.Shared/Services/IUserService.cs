using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IUserService
{
    void Create(User newUser);

    IEnumerable<User> Find(string username);

    IEnumerable<User> GetAll();

    User? GetById(int id);

    User? GetByUsername(string username);

    User UpdateUser(User user);
}
