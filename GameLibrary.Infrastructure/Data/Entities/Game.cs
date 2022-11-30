using GameLibrary.Infrastructure.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.DataConstants.Game;

namespace GameLibrary.Infrastructure.Data.Entities
{
    public class Game
    {
        public Game()
        {
            //GamesComments = new List<GameComment>();
            Comments = new List<Comment>();
            UsersGames = new List<UserGame>();
            UsersGamesForLike = new List<UserGameForLike>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxTitleLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Precision(Precision, Scale)]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(MaxImageLength)]
        public string ImageUrl { get; set; } = null!;


        [Required]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; } = null!;

        [ForeignKey(nameof(Theme))]
        public int ThemeId { get; set; }

        [Required]
        public Theme Theme { get; set; } = null!;

        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }

        public bool HasLiked { get; set; } = false;
        public bool HasDisliked { get; set; } = false;


        public ICollection<Comment> Comments { get; set; }
        public ICollection<UserGame> UsersGames { get; set; }
        public ICollection<UserGameForLike> UsersGamesForLike { get; set; }
    }
}
