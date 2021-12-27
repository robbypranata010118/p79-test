using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P79.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P79.Api.Admin
{
    public static class CorsServiceCollection
    {
        private const string APP_SETTINGS_CORS_SECTION = "Cors";

        public static IServiceCollection AddOwnCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            CorsSetting corsSetting = new CorsSetting();
            configuration.Bind(APP_SETTINGS_CORS_SECTION, corsSetting);
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", policy =>
                {
                    policy.WithOrigins(corsSetting.Origins.ToArray())
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
