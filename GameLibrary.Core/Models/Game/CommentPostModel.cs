using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.DataConstants.Comment;

namespace GameLibrary.Core.Models.Game
{
    public class CommentPostModel : GameViewModel
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        [StringLength(MaxDescriptionLength)]
        public string CommentDescription { get; set; } = null!;

        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string UserName { get; set; } = null!;
    }
}
