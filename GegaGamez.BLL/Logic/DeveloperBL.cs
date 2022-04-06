using GegaGamez.BLL.Models;
using GegaGamez.DAL.Services;
using GegaGamez.DAL.Services.EFCore;

namespace GegaGamez.BLL.Logic
{
    public class DeveloperBL : IDisposable
    {
        private readonly IUnitOfWork _db;

        public DeveloperBL(string connectionString)
        {
            _db = new UnitOfWork(connectionString);
        }

        public IEnumerable<Developer> ListAll()
        {
            IEnumerable<Developer> output;

            var developers = _db.Developers.List();

            output = AutoMapping.Mapper.Map<IEnumerable<Developer>>(developers);
            return output;
        }

        public IEnumerable<Developer> FindByName(string name)
        {
            IEnumerable<Developer> output;
            var developersByName = _db.Developers.GetAllByName(name);

            output = AutoMapping.Mapper.Map<IEnumerable<Developer>>(developersByName);
            return output;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
