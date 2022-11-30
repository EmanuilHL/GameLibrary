﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Infrastructure.Data.Entities
{
    public class GameMechanic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;


        [Required]
        public int HelperId { get; set; }
        [ForeignKey(nameof(HelperId))]
        public Helper Helper { get; set; } = null!;


    }
}