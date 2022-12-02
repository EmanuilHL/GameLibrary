using System;
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
        /// <summary>
        /// ONE develepor shoulkd have MANY game mechanic posts
        /// </summary>
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string GameName { get; set; } = null!;

        [Required]
        [StringLength(400)]
        public string MechanicDescription { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        [Required]
        public User User { get; set; } = null!;


        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }
        [Required]
        public Game Game { get; set; } = null!;
    }
}
