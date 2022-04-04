using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Genre
    {
        private IUnitOfWork _db;

        public Genre(IUnitOfWork db)
        {
            _db = db;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
