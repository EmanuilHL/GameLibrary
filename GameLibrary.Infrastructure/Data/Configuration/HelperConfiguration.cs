using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Infrastructure.Data.Configuration
{
    public class HelperConfiguration : IEntityTypeConfiguration<Helper>
    {
        public void Configure(EntityTypeBuilder<Helper> builder)
        {
            builder.HasData(SeedHelpers());
        }

        private List<Helper> SeedHelpers()
        {
            var helpers = new List<Helper>();

            var helper = new Helper()
            {
                Id = 3,
                PhoneNumber = "+359134554324",
                UserId = "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e"
            };


            helpers.Add(helper);

           

            return helpers;
        }
    }
}
