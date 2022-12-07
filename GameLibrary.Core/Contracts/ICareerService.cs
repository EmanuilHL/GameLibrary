using GameLibrary.Core.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Contracts
{
    public interface ICareerService
    {
        Task<bool> ExistsById(string userId);
        Task<bool> HelperWithPhoneNumberExists(string phoneNumber);
        Task CreateHelper(string userId, string phoneNumber);

        Task<IEnumerable<HelperAdminServiceModel>> AllHelpers();
    }
}
