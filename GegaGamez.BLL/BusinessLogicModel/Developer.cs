using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Developer
    {
        private IUnitOfWork _db;

        public Developer(IUnitOfWork db)
        {
            _db = db;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
