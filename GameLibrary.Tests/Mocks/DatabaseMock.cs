using GameLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static ApplicationDbContext Data
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("GameLibraryInMemoryDb" + DateTime.Now.Ticks.ToString())
                    .Options;

                return new ApplicationDbContext(dbContextOptions, false); 
            }
        }
    }
}
