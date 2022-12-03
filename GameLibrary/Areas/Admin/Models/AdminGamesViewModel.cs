using GameLibrary.Core.Models.Game;
using GameLibrary.Infrastructure.Data.Entities;

namespace GameLibrary.Areas.Admin.Models
{
    public class AdminGamesViewModel
    {
        public IEnumerable<GameViewModel> GamesAddedByAdmin { get; set; } = new List<GameViewModel>();
    }
}
