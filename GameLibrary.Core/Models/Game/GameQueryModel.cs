using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.Game
{
    public class GameQueryModel
    {
        public IEnumerable<GameViewModel> Games { get; set; } = new List<GameViewModel>();
    }
}
