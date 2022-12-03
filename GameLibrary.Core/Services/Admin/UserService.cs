using GameLibrary.Core.Contracts.Admin;
using GameLibrary.Core.Models.Admin;
using GameLibrary.Infrastructure.Data.Common;
using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        public UserService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<Helper>> AllHelpers()
        {
            return (IEnumerable<Helper>)await repo.AllReadonly<Helper>()
                .Select(x => new HelperServiceModel()
                {
                    HelperId = x.Id,
                    PhoneNumber = x.PhoneNumber,
                    UserId = x.UserId
                }).ToListAsync();
        }

        public async Task<IEnumerable<User>> AllUsers()
        {
            return (IEnumerable<User>)await repo.AllReadonly<User>()
                .Select(x => new UserServiceModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserId = x.Id
                }).ToListAsync();
        }
    }
}
