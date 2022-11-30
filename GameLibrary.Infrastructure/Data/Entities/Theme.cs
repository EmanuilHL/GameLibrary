using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.DataConstants.Theme;

namespace GameLibrary.Infrastructure.Data.Entities
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxThemeNameLength)]
        public string ThemeName { get; set; } = null!;
    }
}
