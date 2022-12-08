using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.Game
{
    public class CommentSectionModel
    {
        public int CommentId { get; set; }

        public string CommentDescription { get; set; } = null!;

        public string UserName { get; set; } = null!;
    }
}
