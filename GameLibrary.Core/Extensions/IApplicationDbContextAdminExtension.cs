using GameLibrary.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameLibrary.Infrastructure.Data.Constants.AdminConstants;


namespace GameLibrary.Core.Extensions
{
    public static class IApplicationDbContextAdminExtension
    {

        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminRole))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdminRole };

                    await roleManager.CreateAsync(role);

                    var admin = await userManager.FindByNameAsync(AdminEmail);

                    await userManager.AddToRoleAsync(admin, role.Name);
                })
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
