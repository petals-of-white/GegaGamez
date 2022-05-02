using GegaGamez.BLL.Services;
using GegaGamez.DAL;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GegaGamez.BLL
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds EFCore UnitOfWork (SQL Server)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddGegaGamezDB(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>(serviceProv => new UnitOfWork(connectionString));
            return services;
        }

        /// <summary>
        /// Adds db-driven services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGegaGamezServices(this IServiceCollection services)
        {
            services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IGameService, GameService>()
                .AddTransient<IGameCollectionService, GameCollectionService>()
                .AddTransient<IGenreService, GenreService>()
                .AddTransient<IDeveloperService, DeveloperService>()
                .AddTransient<ICountryService, CountryService>();

            return services;
        }
    }
}
