using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Services
{
    public interface IDeveloperService
    {
        IEnumerable<Developer> Find(string byName = "");

        IEnumerable<Developer> FindAll();

        Developer? GetById(int id);

        void LoadGames(Developer developer);
    }
}
