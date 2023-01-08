
using GameLibrary.Core.Contracts.Admin;
using GameLibrary.Core.Models.Admin;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.GameDeveloperConstants;

namespace GameLibrary.Core.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        private readonly IMemoryCache cache;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<User> userManager;

        public const string UsersCacheKey = "UsersCacheKey";

        public UserService(
            IRepository _repo,
            IMemoryCache _cache,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            repo = _repo;
            cache = _cache;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<UserServiceModel>> AllUsers()
        {
            var cachedUsers = cache.Get<IEnumerable<UserServiceModel>>(UsersCacheKey);

            if (cachedUsers == null)
            {
                cachedUsers = await repo.All<User>()
                    .Select(x => new UserServiceModel()
                    {
                        Email = x.Email,
                        UserName = x.UserName,
                        UserId = x.Id
                    }).ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(3));

                cache.Set(UsersCacheKey, cachedUsers, cacheEntryOptions);

                return cachedUsers
                    .Where(x => x.UserName.Split('-')[0] != "forgottenUser");
            }
            else
            {
                return cachedUsers.Where(x => x.UserName.Split('-')[0] != "forgottenUser");
            }
        }

        public async Task<bool> Forget(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            user.PhoneNumber = null;
            user.Email = null;
            user.NormalizedEmail = null;
            user.NormalizedUserName = null;
            user.PasswordHash = null;
            user.UserName = $"forgottenUser-{DateTime.Now.Ticks}";
            user.DeveloperId = null;

            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task ApplyRoleToDeveloper(RankFormModel model)
        {
            //Random random = new Random();
            //int maximum = 10000;
            var developer = await repo.All<User>().FirstOrDefaultAsync(x => x.UserName == model.UserName);

            if (developer == null)
            {
                throw new ArgumentException("Username not found");
            }

            if (!await roleManager.RoleExistsAsync(GameDeveloperRole))
            {
                var role = new IdentityRole { Name = GameDeveloperRole };

                await roleManager.CreateAsync(role);
            }

            var game = await repo.All<Game>().FirstOrDefaultAsync(x => x.Title == model.GameName);

            if (game == null)
            {
                throw new ArgumentException("Game not found");
            }

            //developer.DeveloperId = string.Concat(game.UserId, "/", random.Next(maximum));
            developer.DeveloperId = game.UserId;

            if (!developer.DevelopersGames.Any(g => g.GameId == game.Id))
            {
                developer.DevelopersGames.Add(new DeveloperGame()
                {
                    Game = game,
                    GameId = game.Id,
                    UserId = developer.Id,
                    User = developer
                });
            }

            await userManager.AddToRoleAsync(developer, GameDeveloperRole);
            await repo.SaveChangesAsync();
        }
    }
}
