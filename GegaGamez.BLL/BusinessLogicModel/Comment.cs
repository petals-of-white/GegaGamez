using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Comment
    {
        private IUnitOfWork _db;

        public Comment(IUnitOfWork db)
        {
            _db = db;
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public UserModel User { get; set; }
    }
}
