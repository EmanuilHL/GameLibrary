using GameLibrary.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.DataConstants.Game;

namespace GameLibrary.Core.Models.Game
{
    public class GameFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTitleLength, MinimumLength = MinTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "0.0", "10.0")]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(MaxImageLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int GenreId { get; set; }

        [Required]
        public int ThemeId { get; set; }

        public string UserId { get; set; } = null!;


        public IEnumerable<Theme> Themes { get; set; } = new List<Theme>();
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    }
}
