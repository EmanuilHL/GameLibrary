using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.DataConstants.Genre;

namespace GameLibrary.Infrastructure.Data.Entities
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxGenreNameLength)]
        public string GenreName { get; set; } = null!;
    }
}
