using System.Text.Json;
using GegaGamez.WebUI.Converters;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GegaGamez.WebUI
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDateOnlyConverters(this JsonSerializerOptions options)
        {
            options.Converters.Add(new DateOnlyConverter());
            options.Converters.Add(new DateOnlyNullableConverter());
        }

        public static IServiceCollection AddGegaGamezSecurity(this IServiceCollection services)
        {
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Login";
                        options.AccessDeniedPath = "/AccessDenied";
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.UserPolicy, config =>
                {
                    config.RequireRole(Roles.User);
                });

                options.AddPolicy(PolicyNames.AdminPolicy, config =>
                {
                    config.RequireRole(Roles.Admin);
                });
            });

            return services;
        }
    }
}
