using System.ComponentModel.DataAnnotations;
using GegaGamez.DAL.Services;

namespace GegaGamez.BLL.BusinessLogicModel
{
    public class DefaultCollectionType
    {
        private IUnitOfWork _db;

        public DefaultCollectionType(IUnitOfWork db)
        {
            _db = db;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
