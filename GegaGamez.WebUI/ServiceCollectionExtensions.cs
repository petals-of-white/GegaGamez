using System.Text;
using System.Text.Json;
using GegaGamez.WebUI.Auth;
using GegaGamez.WebUI.Converters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GegaGamez.WebUI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGegaGamezSecurity(this IServiceCollection services, string securityKey)
        {
            services.AddScoped<IJwtAuthenticationManager>(service =>
            {
                return new JwtAuthenticationManager(securityKey);
            });

            services.AddScoped<IAuthManager, AuthManager>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie()

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

            return services;
        }

        public static void AddDateOnlyConverters(this JsonSerializerOptions options)
        {
            options.Converters.Add(new DateOnlyConverter());
            options.Converters.Add(new DateOnlyNullableConverter());
        }
    }
}
