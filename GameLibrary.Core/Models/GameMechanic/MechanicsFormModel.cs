using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.GameMechanic
{
    public class MechanicsFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string GameName { get; set; } = null!;

        [Required]
        [StringLength(400, MinimumLength = 30)]
        public string MechanicDescription { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;


        public int GameId { get; set; }
    }
}
