using GameLibrary.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.GameMechanic
{
    public class MechanicsViewModel
    {
        public string GameName { get; set; } = null!;

        public string MechanicDescription { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public int GameId { get; set; }

        public int MechanicId { get; set; }
    }
}
