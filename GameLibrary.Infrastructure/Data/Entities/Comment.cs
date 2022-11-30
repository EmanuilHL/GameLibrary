using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.DataConstants.Comment;

namespace GameLibrary.Infrastructure.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Required]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Game))]
        public int? GameId { get; set; }
        public Game Game { get; set; } = null!;
    }
}
