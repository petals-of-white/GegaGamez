using System.Text.Json;
using GegaGamez.WebUI.Converters;
using GegaGamez.WebUI.Security;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GegaGamez.WebUI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGegaGamezSecurity(this IServiceCollection services)
        {
            //services.AddScoped<IJwtAuthenticationManager>(service =>
            //{
            //    return new JwtAuthenticationManager(securityKey);
            //});

            services.AddScoped<IAuthManager, AuthManager>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Login";
                        options.AccessDeniedPath = "/AccessDenied";
                    });

            /*
            .AddJwtBearer((o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies [JwtAuthenticationManager.CookieName];
                        return Task.CompletedTask;
                    }
                };
            }));
            */

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

        public static void AddDateOnlyConverters(this JsonSerializerOptions options)
        {
            options.Converters.Add(new DateOnlyConverter());
            options.Converters.Add(new DateOnlyNullableConverter());
        }
    }
}
