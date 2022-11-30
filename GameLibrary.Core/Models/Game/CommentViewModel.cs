using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.Game
{
    public class CommentViewModel : GameViewModel
    {
        public IEnumerable<CommentFormModel> Comments { get; set; } = new List<CommentFormModel>();
    }
}
