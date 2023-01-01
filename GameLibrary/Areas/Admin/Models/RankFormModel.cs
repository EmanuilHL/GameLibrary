using System.ComponentModel.DataAnnotations;

namespace GameLibrary.Areas.Admin.Models
{
    public class RankFormModel
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string UserName { get; set; } = null!;
    }
}
