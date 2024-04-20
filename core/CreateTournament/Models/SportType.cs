﻿using System.ComponentModel.DataAnnotations;

namespace CreateTournament.Models
{
    public class SportType : DeleteableEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
