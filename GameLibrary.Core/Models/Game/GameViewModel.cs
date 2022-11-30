using GameLibrary.Infrastructure.Data.Entities;
using GameLibrary.Infrastructure.Data.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Models.Game
{
	/// <summary>
	/// Make the model view.
	/// its just to show the users and guests what games there are, so it doesn't require any validations.
	/// </summary>
	public class GameViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public string Description { get; set; } = null!;

        public decimal Rating { get; set; }
        public decimal BaseRating { get; set; }
		public bool IsFirst { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string Genre { get; set; } = null!;

		public string UserId { get; set; } = null!;

		public string ReviewType { get; set; } = null!;

        public int LikesCount { get; set; }

        public int DislikesCount { get; set; }

        public IEnumerable<Theme> Themes { get; set; } = new List<Theme>();
	}
}
