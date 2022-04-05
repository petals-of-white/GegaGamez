﻿using System.ComponentModel.DataAnnotations;

namespace GegaGamez.BLL.Models
{
    public class Developer : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
