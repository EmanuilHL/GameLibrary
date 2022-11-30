using GameLibrary.Core.Models.Game;
using GameLibrary.Infrastructure.Data.Entities;

namespace GameLibrary.Core.Models
{
    public class SearchQueryViewModel
    {

        public string? Theme { get; set; }

        public string? SearchTerm { get; set; }

        public RatingSorting Sorting { get; set; }

        public IEnumerable<Theme> Themes { get; set; } = Enumerable.Empty<Theme>();

        public IEnumerable<GameViewModel> Games { get; set; } = Enumerable.Empty<GameViewModel>();
    }
}
