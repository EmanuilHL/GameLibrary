using GameLibrary.Core.Contracts;
using GameLibrary.Core.Services;
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

            return services;
        }
    }
}
