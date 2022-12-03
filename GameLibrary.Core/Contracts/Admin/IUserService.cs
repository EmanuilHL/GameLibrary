using GameLibrary.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Contracts.Admin
{
    public interface IUserService
    {
        Task<IEnumerable<User>> AllUsers();
        Task<IEnumerable<Helper>> AllHelpers();
    }
}
