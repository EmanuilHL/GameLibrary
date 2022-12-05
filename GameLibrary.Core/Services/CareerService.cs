using GameLibrary.Core.Contracts;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
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

        public CareerService(IRepository repo)
        {
            this.repo = repo;
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
    }
}
