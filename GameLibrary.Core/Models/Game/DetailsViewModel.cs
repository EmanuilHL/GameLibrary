using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.Game
{
    public class DetailsViewModel : GameViewModel
    {
        public string PageOwnerName { get; set; } = null!;
        public IEnumerable<CommentSectionModel> Comments { get; set; } = new List<CommentSectionModel>();
    }
}
