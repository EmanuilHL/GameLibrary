using GameLibrary.Core.Contracts;
using GameLibrary.Core.Contracts.Admin;
using GameLibrary.Core.Services;
using GameLibrary.Core.Services.Admin;
using GameLibrary.Infrastructure.Data.Common;

namespace GameLibrary.Extensions
{
    public static class GameLibraryServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ICareerService, CareerService>();
            services.AddScoped<IGameMechanicService, GameMechanicService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
