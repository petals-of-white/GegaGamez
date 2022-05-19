using GegaGamez.BLL.Services;
using GegaGamez.DAL;
using GegaGamez.DAL.Data;
using GegaGamez.Shared.DataAccess;
using GegaGamez.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GegaGamez.BLL
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds EFCore IUnitOfWork
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddGegaGamezDB(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("GegaGamezDev");

            // EFCore SqlServer
            services.AddSqlServer<GegaGamezContext>(connectionString,optionsAction: builder=>builder.EnableSensitiveDataLogging());

            // UoW implementation
            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
                .AddTransient<ICountryService, CountryService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IStatisticsService, StatisticsService>()
                .AddTransient<IRatingService, RatingService>();

            return services;
        }
    }
}
