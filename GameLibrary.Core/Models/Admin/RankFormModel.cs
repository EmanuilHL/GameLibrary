using System.ComponentModel.DataAnnotations;

namespace GameLibrary.Core.Models.Admin
{
    public class RankFormModel
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string GameName { get; set; } = null!;
    }
}
