using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Infrastructure.Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<UserGame> UsersGames { get; set; } = new List<UserGame>();
        public ICollection<UserGameForLike> UsersGamesForLike { get; set; } = new List<UserGameForLike>();
        public ICollection<DeveloperGame> DevelopersGames { get; set; } = new List<DeveloperGame>();
        public string? DeveloperId { get; set; }
    }
}
