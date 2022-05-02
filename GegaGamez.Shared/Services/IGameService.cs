using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services;

public interface IGameService : IDisposable
{
    IEnumerable<Game> Find(string? byTitle, params Genre [] byGenre);

    IEnumerable<Game> GetAll();

    Game? GetById(int id);

    void LoadGameComments(Game game);

    void LoadGameGenres(Game game);
}
