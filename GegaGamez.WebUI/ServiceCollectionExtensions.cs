using System.Text;
using System.Text.Json;
using GegaGamez.WebUI.Auth;
using GegaGamez.WebUI.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GegaGamez.WebUI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string securityKey)
        {
            services.AddScoped<IJwtAuthenticationManager>(service =>
            {
                return new JwtAuthenticationManager(securityKey);
            })

                    .AddAuthentication(x =>
                        {
                            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                            //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })

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
                                     context.Token = context.Request.Cookies ["jwt"];
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
