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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(SeedUsers());
        }

        private List<User> SeedUsers()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "pesho",
                NormalizedUserName = "PESHO",
                Email = "agent@mail.com",
                NormalizedEmail = "AGENT@MAIL.COM",
                DeveloperId = null
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "agent123");

            users.Add(user);

            user = new User()
            {
                Id = "85601b02-9a83-47d0-b4a2-fcd5c6c16f1e",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                DeveloperId = null
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "admin123");

            users.Add(user);

            return users;
        }
    }
}
