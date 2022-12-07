using GameLibrary.Core.Contracts.Admin;
using GameLibrary.Core.Models.Admin;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        private readonly IMemoryCache cache;

        public const string UsersCacheKey = "UsersCacheKey";

        public UserService(
            IRepository _repo,
            IMemoryCache _cache)
        {
            repo = _repo;
            cache = _cache;
        }

        public async Task<IEnumerable<UserServiceModel>> AllUsers()
        {

            var cachedUsers = cache.Get<IEnumerable<UserServiceModel>>(UsersCacheKey);

            if (cachedUsers == null)
            {
                cachedUsers = await repo.AllReadonly<User>()
                .Select(x => new UserServiceModel()
                {
                    Email = x.Email,
                    UserName = x.UserName,
                    UserId = x.Id
                }).ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(20));

                cache.Set(UsersCacheKey, cachedUsers, cacheEntryOptions);

                return cachedUsers;
            }
            else
            {
                return cachedUsers;
            }
        }
    }
}
