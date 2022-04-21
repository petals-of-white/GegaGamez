﻿using System.ComponentModel.DataAnnotations;
using GegaGamez.Shared.Validation;

namespace GegaGamez.Shared.BusinessModels
{
    public class Rating : ValidatableModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 10)]
        public byte RatingScore { get; set; }

        [Required]
        public Game Game { get; set; }

        [Required]
        public User User { get; set; } = null!;
    }
}