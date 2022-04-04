using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class Country
    {
        private IUnitOfWork _db;

        public Country(IUnitOfWork db)
        {
            _db = db;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string TwoCharCode { get; set; }

        public string ThreeCharCode { get; set; }
    }
}
