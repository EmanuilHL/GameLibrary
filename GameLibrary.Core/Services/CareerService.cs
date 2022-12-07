using GameLibrary.Core.Contracts;
using GameLibrary.Core.Models.Admin;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Services
{
    public class CareerService : ICareerService
    {
        private readonly IRepository repo;
        private readonly IMemoryCache cache;

        private const string HelpersCacheKey = "HelpersCacheKey";

        public CareerService(
            IRepository repo,
            IMemoryCache cache)
        {
            this.repo = repo;
            this.cache = cache;
        }

        public async Task CreateHelper(string userId, string phoneNumber)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();
            var helper = new Helper()
            {
                UserId = userId,
                PhoneNumber = sanitizer.Sanitize(phoneNumber)
            };

            await repo.AddAsync(helper);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await repo.All<Helper>().AnyAsync(x => x.UserId == userId);
        }

        public async Task<bool> HelperWithPhoneNumberExists(string phoneNumber)
        {
            return await repo.All<Helper>().AnyAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<HelperAdminServiceModel>> AllHelpers()
        {
            var cachedHelpers = cache.Get<IEnumerable<HelperAdminServiceModel>>(HelpersCacheKey);

            if (cachedHelpers == null)
            {
                cachedHelpers = await repo.AllReadonly<Helper>()
                .Select(x => new HelperAdminServiceModel()
                {
                    HelperId = x.Id,
                    PhoneNumber = x.PhoneNumber,
                    UserId = x.UserId
                }).ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(20));

                cache.Set(HelpersCacheKey, cachedHelpers, cacheEntryOptions);

                return cachedHelpers;
            }
            else
            {
                return cachedHelpers;
            }
        }
    }
}
