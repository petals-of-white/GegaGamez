using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GegaGamez.DAL.Entities
{
    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [StringLength(2)]
        [Unicode(false)]
        public string TwoCharCode { get; set; } = null!;

        [StringLength(3)]
        [Unicode(false)]
        public string ThreeCharCode { get; set; } = null!;

        [InverseProperty("Country")]
        public virtual ICollection<User> Users { get; set; }
    }
}