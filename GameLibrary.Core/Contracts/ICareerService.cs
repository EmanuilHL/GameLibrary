﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Core.Contracts
{
    public interface ICareerService
    {
        Task<bool> ExistsById(string userId);
        Task<bool> UserWithPhoneNumberExists(string phoneNumber);
        Task CreateHelper(string userId, string phoneNumber);
    }
}