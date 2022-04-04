using GegaGamez.BLL.BusinessLogicModel;
using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.Searchers
{
    public class DbSearcher
    {
        private IUnitOfWork _db;

        public DbSearcher(IUnitOfWork db)
        {
            _db = db;
        }

        public IEnumerable<UserModel> FindUsersByUsername(string username)
        {
            return _db.Users.GetAllByUsername(username).Select();
        }
    }
}
